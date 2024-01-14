using AutoMapper;
using Haskap.DddBase.Domain.Providers;
using Haskap.Workflow.Application.Contracts.Processes.Process1;
using Haskap.Workflow.Application.Dtos.Processes.Process1;
using Haskap.Workflow.Domain;
using Haskap.Workflow.Domain.Process1Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haskap.Workflow.Domain;
using Haskap.Workflow.Domain.Shared.Enums;

namespace Haskap.Workflow.Application.UseCaseServices.Processes.Process1;
public class Process1Service : IProcess1Service
{
    private readonly IMapper _mapper;
    private readonly IWorkflowDbContext _workflowDbContext;
    private readonly ICurrentUserIdProvider _currentUserIdProvider;

    public Process1Service(IMapper mapper, IWorkflowDbContext workflowDbContext, ICurrentUserIdProvider currentUserIdProvider)
    {
        _mapper = mapper;
        _workflowDbContext = workflowDbContext;
        _currentUserIdProvider = currentUserIdProvider;
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
        var process1RequestData = new Process1RequestData(requestId, inputDto.FirstName, inputDto.LastName);
        await _workflowDbContext.Process1RequestData.AddAsync(process1RequestData, cancellationToken);

        await _workflowDbContext.SaveChangesAsync(cancellationToken);

        return requestId;
    }
}
