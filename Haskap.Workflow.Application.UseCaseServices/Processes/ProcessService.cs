using Haskap.Workflow.Application.Contracts.Processes;
using Haskap.Workflow.Application.Dtos.Processes;
using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haskap.DddBase.Domain.Providers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Haskap.Workflow.Domain.Shared.Enums;
using System.Diagnostics;
using System.ComponentModel.Design;
using Haskap.Workflow.Application.Dtos.Processes.Process1;
using Haskap.Workflow.Domain.Process1Aggregate;
using Haskap.Workflow.Domain.Common;

namespace Haskap.Workflow.Application.UseCaseServices.Processes;
public class ProcessService : IProcessService
{
    private readonly IMapper _mapper;
    private readonly IWorkflowDbContext _workflowDbContext;
    private readonly ICurrentUserIdProvider _currentUserIdProvider;

    public ProcessService(
        IWorkflowDbContext workflowDbContext,
        ICurrentUserIdProvider currentUserIdProvider,
        IMapper mapper)
    {
        _workflowDbContext = workflowDbContext;
        _currentUserIdProvider = currentUserIdProvider;
        _mapper = mapper;
    }



    public async Task<GetAvailablePathsOutputDto> GetAvailablePathsAsync(GetAvailablePathsInputDto inputDto, CancellationToken cancellationToken)
    {
        var currentState = await _workflowDbContext.Request
            .Include(x => x.CurrentState)
            .Where(x => x.Id == inputDto.RequestId)
            .Select(x => x.CurrentState)
            .FirstAsync(cancellationToken);

        var userRoleIds = await _workflowDbContext.UserRole
            .Where(x => x.UserId == _currentUserIdProvider.CurrentUserId)
            .Select(x => x.RoleId)
            .ToListAsync(cancellationToken);

        var availablePaths = await _workflowDbContext.Path
            .Include(x => x.Command)
            .Include(x => x.Roles)
            .Where(x => x.FromStateId == currentState.Id && x.Roles.Any(y => userRoleIds.Contains(y.RoleId)))
            .ToListAsync(cancellationToken);

        var outputDto = new GetAvailablePathsOutputDto
        {
            AvailablePaths = _mapper.Map<List<PathOutputDto>>(availablePaths)
        };

        return outputDto;
    }

    public async Task<Guid> InitRequestAsync(Guid processId, dynamic? requestData, CancellationToken cancellationToken)
    {
        var process = await _workflowDbContext.Process
            .Include(x => x.States.Where(y => y.StateType == StateType.StartState))
            .Where(x => x.Id == processId)
            .FirstAsync(cancellationToken);

        var requestId = process.InitRequest(_currentUserIdProvider.CurrentUserId.Value);

        // save data with requestId,
        // data comes with inputDto,
        // then SaveChanges (in single transaction)
        if (requestData is not null)
        {
            requestData.RequestId = requestId;
            //var process1RequestData = new RequestData(requestId, inputDto.FirstName, inputDto.LastName);
            await _workflowDbContext.AddAsync(requestData, cancellationToken);
        }

        await _workflowDbContext.SaveChangesAsync(cancellationToken);

        return requestId;
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

        var progressId = request.MakeProgress(path.ToState, path.Id, _currentUserIdProvider.CurrentUserId.Value);

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


}
