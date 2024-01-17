using Haskap.Workflow.Application.Dtos.Common.DataTable;
using Haskap.Workflow.Application.Dtos.Processes;
using Haskap.Workflow.Application.Dtos.Processes.Process1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Contracts.Processes.Process1;
public interface IProcess1Service
{
    Task DeleteRequestAsync(DeleteRequestInputDto inputDto, CancellationToken cancellationToken);
    Task<RequestDetailOutputDto> GetRequestDetailAsync(Guid requestId, CancellationToken cancellationToken);
    Task<JqueryDataTableResult> SearchRequestAsync(SearchParamsInputDto inputDto, JqueryDataTableParam jqueryDataTableParam, CancellationToken cancellationToken);
    Task<Guid> CreateRequest(Guid processId, RequestDataInputDto requestDataInputDto, CancellationToken cancellationToken);
    Task<List<PathOutputDto>> GetAvailablePathsAsync(Guid requestId, CancellationToken cancellationToken);
    Task MakeProgressAsync(MakeProgressInputDto inputDto, CancellationToken cancellationToken);
    Task MakeProgressWithNoteAsync(MakeProgressInputDto inputDto, NoteProgressDataInputDto progressDataInputDto, CancellationToken cancellationToken);
}