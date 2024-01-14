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



    public async Task<GetAvailableCommandsOutputDto> GetAvailableCommandsAsync(GetAvailableCommandsInputDto inputDto, CancellationToken cancellationToken)
    {
        var currentState = await _workflowDbContext.Request
            .Include(x => x.CurrentState)
            .Where(x => x.Id == inputDto.RequestId)
        .Select(x => x.CurrentState)
            .FirstAsync(cancellationToken);

        var availableCommands = await _workflowDbContext.Path
            .Include(x => x.Command)
            .Where(x => x.FromStateId == currentState.Id)
            .Select(x => x.Command)
            .ToListAsync(cancellationToken);

        var outputDto = new GetAvailableCommandsOutputDto
        {
            AvailableCommands = _mapper.Map<List<CommandOutputDto>>(availableCommands)
        };

        return outputDto;
    }

    public async Task<Guid> InitRequestAsync(InitRequestInputDto inputDto, CancellationToken cancellationToken)
    {
        var process = await _workflowDbContext.Process
            .Include(x => x.States.Where(y => y.StateType == StateType.StartState))
            .Where(x => x.Id == inputDto.ProcessId)
            .FirstAsync(cancellationToken);

        var requestId = process.InitRequest(_currentUserIdProvider.CurrentUserId.Value);

        // save data with requestId,
        // data comes with inputDto,
        // then SaveChanges (in single transaction)


        await _workflowDbContext.SaveChangesAsync(cancellationToken);

        return requestId;
    }

    public async Task<Guid> MakeProgressAsync(MakeProgressInputDto inputDto, CancellationToken cancellationToken)
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

        await _workflowDbContext.SaveChangesAsync(cancellationToken);

        return progressId;
    }
}
