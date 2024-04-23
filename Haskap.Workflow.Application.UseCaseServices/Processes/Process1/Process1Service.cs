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
using Haskap.DddBase.Application.Dtos.Common.DataTable;
using Haskap.Workflow.Application.Dtos.Processes;
using Haskap.DddBase.Utilities.Guids;
using Haskap.Workflow.Domain.ProcessAggregate;

namespace Haskap.Workflow.Application.UseCaseServices.Processes.Process1;
public class Process1Service : IProcess1Service
{
    private readonly IMapper _mapper;
    private readonly IWorkflowDbContext _workflowDbContext;
    private readonly ICurrentUserIdProvider _currentUserIdProvider;
    private readonly ProcessDomainService _processDomainService;

    public Process1Service(
        IMapper mapper,
        IWorkflowDbContext workflowDbContext,
        ICurrentUserIdProvider currentUserIdProvider,
        ProcessDomainService processDomainService)
    {
        _mapper = mapper;
        _workflowDbContext = workflowDbContext;
        _currentUserIdProvider = currentUserIdProvider;
        _processDomainService = processDomainService;
    }

    public async Task<Guid> CreateRequest(Guid processId, RequestDataInputDto requestDataInputDto, CancellationToken cancellationToken)
    {
        var requestData = new RequestData(GuidGenerator.CreateSimpleGuid(), requestDataInputDto.FirstName, requestDataInputDto.LastName);
        var requestId = await _processDomainService.InitRequestAsync(processId, requestData, cancellationToken);

        return requestId;
    }

    public async Task DeleteRequestAsync(DeleteRequestInputDto inputDto, CancellationToken cancellationToken)
    {
        await _workflowDbContext.Request
            .Where(x => x.Id == inputDto.RequestId)
            .ExecuteDeleteAsync();
    }

    public async Task<List<PathOutputDto>> GetAvailablePathsAsync(Guid requestId, CancellationToken cancellationToken)
    {
        var availablePaths = await _processDomainService.GetAvailablePathsAsync(requestId, cancellationToken);

        return _mapper.Map<List<PathOutputDto>>(availablePaths);
    }

    public async Task<RequestDetailOutputDto> GetRequestDetailAsync(Guid requestId, CancellationToken cancellationToken)
    {
        var output = await (from request in _workflowDbContext.Request.Include(x => x.CurrentState)
                            join user in _workflowDbContext.User on request.OwnerUserId equals user.Id
                            join requestData in _workflowDbContext.Process1RequestData on request.Id equals requestData.RequestId
                            where request.Id == requestId
                            select new RequestDetailOutputDto
                            {
                                Id = request.Id,
                                Data = new RequestDataOutputDto
                                {
                                    FirstName = requestData.FirstName,
                                    LastName = requestData.LastName,
                                },
                                CurrentState = new Dtos.Processes.StateOutputDto
                                {
                                    Id = request.CurrentStateId,
                                    DisplayName = request.CurrentState.DisplayName,
                                    ProcessId = request.CurrentState.ProcessId,
                                    StateType = request.CurrentState.StateType
                                },
                                OwnerUserFirstName = user.FirstName,
                                OwnerUserLastName = user.LastName,
                            })
                      .FirstAsync(cancellationToken);

        return output;
    }

    public async Task MakeProgressAsync(MakeProgressInputDto inputDto, CancellationToken cancellationToken)
    {
        await _processDomainService.MakeProgressAsync(inputDto, null, cancellationToken);
    }

    public async Task MakeProgressWithNoteAsync(MakeProgressInputDto inputDto, NoteProgressDataInputDto progressDataInputDto, CancellationToken cancellationToken)
    {
        var progressData = new NoteProgressData(GuidGenerator.CreateSimpleGuid(), progressDataInputDto.Note);
        await _processDomainService.MakeProgressAsync(inputDto, progressData, cancellationToken);
    }

    public async Task<JqueryDataTableResult> SearchRequestAsync(SearchParamsInputDto inputDto, JqueryDataTableParam jqueryDataTableParam, CancellationToken cancellationToken)
    {
        var query = (from request in _workflowDbContext.Request.Include(x=>x.CurrentState)
                     join user in _workflowDbContext.User on request.OwnerUserId equals user.Id
                     join requestData in _workflowDbContext.Process1RequestData on request.Id equals requestData.RequestId
                     select new RequestOutputDto
                     {
                         Id = request.Id,
                         Data = new RequestDataOutputDto
                         {
                             FirstName = requestData.FirstName,
                             LastName = requestData.LastName,
                         },
                         CurrentState = new Dtos.Processes.StateOutputDto
                         {
                             Id = request.CurrentStateId,
                             DisplayName = request.CurrentState.DisplayName,
                             ProcessId = request.CurrentState.ProcessId,
                             StateType = request.CurrentState.StateType
                         },
                         OwnerUserFirstName = user.FirstName,
                         OwnerUserLastName = user.LastName,
                     });

        var totalCount = await query.CountAsync(cancellationToken);
        var filteredCount = totalCount;

        var filtered = false;
        if (inputDto.FirstName is not null)
        {
            filtered = true;
            query = query.Where(x => x.Data.FirstName.Contains(inputDto.FirstName));
        }

        if (inputDto.LastName is not null)
        {
            filtered = true;
            query = query.Where(x => x.Data.LastName.Contains(inputDto.LastName));
        }


        if (filtered)
        {
            filteredCount = await query.CountAsync(cancellationToken);
        }

        if (jqueryDataTableParam.Order.Any())
        {
            var direction = jqueryDataTableParam.Order[0].Dir;
            var columnIndex = jqueryDataTableParam.Order[0].Column;

            if (columnIndex == 0)
            {
                if (direction == "asc")
                {
                    query = query.OrderBy(x => x.Data.FirstName);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Data.FirstName);
                }
            }
            else if (columnIndex == 1)
            {
                if (direction == "asc")
                {
                    query = query.OrderBy(x => x.Data.LastName);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Data.LastName);
                }
            }
            else if (columnIndex == 2)
            {
                if (direction == "asc")
                {
                    query = query.OrderBy(x => x.OwnerUserFirstName).ThenBy(x => x.OwnerUserLastName);
                }
                else
                {
                    query = query.OrderByDescending(x => x.OwnerUserFirstName).ThenByDescending(x => x.OwnerUserLastName);
                }
            }
        }
        else
        {
            query = query.OrderBy(x => x.Data.FirstName);
        }

        var skip = jqueryDataTableParam.Start;
        var take = jqueryDataTableParam.Length;

        var requestOutputDtos = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return new JqueryDataTableResult
        {
            // this is what datatables wants sending back
            draw = jqueryDataTableParam.Draw,
            recordsTotal = totalCount,
            recordsFiltered = filteredCount,
            data = requestOutputDtos
        };
    }
}
