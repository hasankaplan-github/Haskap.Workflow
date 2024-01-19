using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Domain.Services;
using Haskap.DddBase.Utilities.Guids;
using Haskap.Workflow.Application.Dtos.Processes;
using Haskap.Workflow.Domain.RequestAggregate;
using Haskap.Workflow.Domain.Shared.Enums;
using Haskap.Workflow.Domain.StateAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.ProcessAggregate;
public class ProcessDomainService : DomainService
{
    private readonly IWorkflowDbContext _workflowDbContext;
    private readonly ICurrentUserIdProvider _currentUserIdProvider;

    public ProcessDomainService(
        IWorkflowDbContext workflowDbContext,
        ICurrentUserIdProvider currentUserIdProvider)
    {
        _workflowDbContext = workflowDbContext;
        _currentUserIdProvider = currentUserIdProvider;
    }

    public async Task<Guid> InitRequestAsync(Guid processId, dynamic? requestData, CancellationToken cancellationToken)
    {
        var startState = _workflowDbContext.State
            .FirstOrDefault(x => x.ProcessId == processId && x.StateType == StateType.StartState);
        var newRequest = new Request(GuidGenerator.CreateSimpleGuid(), processId, _currentUserIdProvider.CurrentUserId.Value, startState);
        await _workflowDbContext.Request.AddAsync(newRequest, cancellationToken);

        // save data with requestId,
        // data comes with inputDto,
        // then SaveChanges (in single transaction)
        if (requestData is not null)
        {
            requestData.RequestId = newRequest.Id;
            //var process1RequestData = new RequestData(requestId, inputDto.FirstName, inputDto.LastName);
            await _workflowDbContext.AddAsync(requestData, cancellationToken);
        }

        await _workflowDbContext.SaveChangesAsync(cancellationToken);

        return newRequest.Id;
    }

    public async Task<Guid> MakeProgressAsync(MakeProgressInputDto inputDto, dynamic? progressData, CancellationToken cancellationToken)
    {
        var request = await _workflowDbContext.Request
            .Where(x => x.Id == inputDto.RequestId)
            .FirstAsync(cancellationToken);

        var path = await _workflowDbContext.Path
            .Include(x => x.ToState)
            .Where(x => x.FromStateId == request.CurrentStateId && x.CommandId == inputDto.CommandId)
            .FirstAsync(cancellationToken);

        var progressId = request.MakeProgress(path, _currentUserIdProvider.CurrentUserId.Value);

        // save data with progressId,
        // data comes with inputDto,
        // then SaveChanges (in single transaction)
        if (progressData is not null)
        {
            progressData.ProgressId = progressId;
            await _workflowDbContext.AddAsync(progressData, cancellationToken);
        }

        await _workflowDbContext.SaveChangesAsync(cancellationToken);

        return progressId;
    }

    public async Task<List<Path>> GetAvailablePathsAsync(Guid requestId, CancellationToken cancellationToken)
    {
        var currentStateId = await _workflowDbContext.Request
            .AsNoTracking()
            .Where(x => x.Id == requestId)
            .Select(x => x.CurrentStateId)
            .FirstAsync(cancellationToken);

        var userRoleIds = await _workflowDbContext.UserRole
            .AsNoTracking()
            .Where(x => x.UserId == _currentUserIdProvider.CurrentUserId)
            .Select(x => x.RoleId)
            .ToListAsync(cancellationToken);

        var availablePaths = await _workflowDbContext.Path
            .AsNoTracking()
            .Include(x => x.Command)
            .Include(x => x.Roles)
            .Where(x => x.FromStateId == currentStateId && x.Roles.Any(y => userRoleIds.Contains(y.RoleId)))
            .ToListAsync(cancellationToken);

        return availablePaths;
    }
}
