using AutoMapper;
using Haskap.Workflow.Application.Dtos.ViewLevelExceptions;
using Haskap.Workflow.Domain.ViewLevelExceptionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.UseCaseServices.Mappings;

internal class ViewLevelExceptionProfile : Profile
{
    public ViewLevelExceptionProfile()
    {
        CreateMap<ViewLevelException, ViewLevelExceptionOutputDto>();
    }
}
