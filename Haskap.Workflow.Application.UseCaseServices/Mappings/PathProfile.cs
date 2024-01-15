using AutoMapper;
using Haskap.Workflow.Application.Dtos.Processes;
using Haskap.Workflow.Domain.ProcessAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.UseCaseServices.Mappings;

internal class PathProfile : Profile
{
    public PathProfile()
    {
        CreateMap<Domain.ProcessAggregate.Path, PathOutputDto>();
    }
}
