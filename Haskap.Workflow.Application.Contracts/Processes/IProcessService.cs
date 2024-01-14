using Haskap.DddBase.Application.Contracts;
using Haskap.Workflow.Application.Dtos.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Contracts.Processes;
public interface IProcessService : IUseCaseService
{
    Task<GetAvailableCommandsOutputDto> GetAvailableCommandsAsync(GetAvailableCommandsInputDto inputDto, CancellationToken cancellationToken);
    Task<Guid> MakeProgressAsync(MakeProgressInputDto inputDto, CancellationToken cancellationToken);
    Task<Guid> InitRequestAsync(InitRequestInputDto inputDto, CancellationToken cancellationToken);
}
