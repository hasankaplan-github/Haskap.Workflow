using AutoMapper;
using Haskap.Workflow.Application.Dtos.Roles;
using Haskap.Workflow.Domain.RoleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.UseCaseServices.Mappings;

internal class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleOutputDto>();
    }
}
