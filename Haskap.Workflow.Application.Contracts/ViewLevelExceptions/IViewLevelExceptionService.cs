using Haskap.Workflow.Application.Dtos.ViewLevelExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Contracts.ViewLevelExceptions;
public interface IViewLevelExceptionService
{
    Task<ViewLevelExceptionOutputDto> GetViewLevelExceptionAsync(Guid id);
    Task DeleteViewLevelExceptionAsync(Guid id);
    Task<Guid> SaveAndGetIdAsync(SaveAndGetIdInputDto inputDto);
}
