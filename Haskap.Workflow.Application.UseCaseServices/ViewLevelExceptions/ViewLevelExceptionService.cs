using AutoMapper;
using Haskap.Workflow.Application.Dtos.ViewLevelExceptions;
using Haskap.Workflow.Domain;
using Haskap.Workflow.Domain.ViewLevelExceptionAggregate;
using Haskap.DddBase.Application.UseCaseServices;
using Haskap.DddBase.Utilities.Guids;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haskap.Workflow.Application.Contracts.ViewLevelExceptions;

namespace Haskap.Workflow.Application.UseCaseServices.ViewLevelExceptions;
public class ViewLevelExceptionService : UseCaseService, IViewLevelExceptionService
{
    private readonly IWorkflowDbContext _ajandaDbContext;
    private readonly IMapper _mapper;

    public ViewLevelExceptionService(
        IWorkflowDbContext ajandaDbContext,
        IMapper mapper)
    {
        _ajandaDbContext = ajandaDbContext;
        _mapper = mapper;
    }

    

    public async Task DeleteViewLevelExceptionAsync(Guid id)
    {
        await _ajandaDbContext.ViewLevelException
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<ViewLevelExceptionOutputDto> GetViewLevelExceptionAsync(Guid id)
    {
        var exception = await _ajandaDbContext.ViewLevelException
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        var output = _mapper.Map<ViewLevelExceptionOutputDto>(exception);

        return output;
    }

    public async Task<Guid> SaveAndGetIdAsync(SaveAndGetIdInputDto inputDto)
    {
        var viewLevelException = new ViewLevelException(GuidGenerator.CreateSimpleGuid())
        {
            Message = inputDto.Message,
            StackTrace = inputDto.StackTrace
        };

        await _ajandaDbContext.ViewLevelException.AddAsync(viewLevelException);
        await _ajandaDbContext.SaveChangesAsync();

        return viewLevelException.Id;
    }
}
