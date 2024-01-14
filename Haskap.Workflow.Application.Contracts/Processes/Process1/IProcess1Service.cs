using Haskap.Workflow.Application.Dtos.Processes.Process1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Contracts.Processes.Process1;
public interface IProcess1Service
{
    Task<Guid> InitRequestAsync(InitRequestInputDto inputDto, CancellationToken cancellationToken);
}
