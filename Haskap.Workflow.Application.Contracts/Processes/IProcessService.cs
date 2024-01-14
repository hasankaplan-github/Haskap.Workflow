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
    Task<GetAvailablePathsOutputDto> GetAvailablePathsAsync(GetAvailablePathsInputDto inputDto, CancellationToken cancellationToken);
    Task<Guid> InitRequestAsync(Guid processId, dynamic? requestData, CancellationToken cancellationToken);
    Task<Guid> MakeProgressAsync(MakeProgressInputDto inputDto, dynamic? progressData, CancellationToken cancellationToken);
}
