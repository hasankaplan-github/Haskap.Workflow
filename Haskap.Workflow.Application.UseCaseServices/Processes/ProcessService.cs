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

    
}
