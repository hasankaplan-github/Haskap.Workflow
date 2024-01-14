using Haskap.DddBase.Application.Contracts;
using Haskap.Workflow.Application.Dtos.Processes;
using Haskap.Workflow.Application.Dtos.Processes.Process1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Contracts.Processes;
public interface IProcessService : IUseCaseService
{
    Task<GetAvailablePathsOutputDto> GetAvailablePathsAsync(GetAvailablePathsInputDto inputDto, CancellationToken cancellationToken);
}
