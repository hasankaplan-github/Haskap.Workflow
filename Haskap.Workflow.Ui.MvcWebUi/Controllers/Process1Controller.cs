using Haskap.Workflow.Application.Contracts.Processes;
using Haskap.Workflow.Application.Contracts.Processes.Process1;
using Haskap.Workflow.Application.Dtos.Common.DataTable;
using Haskap.Workflow.Application.Dtos.Processes;
using Haskap.Workflow.Application.Dtos.Processes.Process1;
using Haskap.Workflow.Domain.Process1Aggregate;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Haskap.Workflow.Ui.MvcWebUi.Controllers;
public class Process1Controller : Controller
{
    private readonly Guid _process1Id = Guid.Parse("");
    private readonly IProcess1Service _process1Service;
    private readonly IProcessService _processService;

    public Process1Controller(IProcess1Service process1Service, IProcessService processService)
    {
        _process1Service = process1Service;
        _processService = processService;
    }

    public async Task<IActionResult> CreateRequest(CancellationToken cancellationToken = default)
    {
        ViewBag.Process1Id = _process1Id;
        return View();
    }

    [HttpPost]
    public async Task CreateRequest(Guid processId, RequestDataInputDto requestDataInputDto, CancellationToken cancellationToken = default)
    {
        var requestData = new RequestData(requestDataInputDto.FirstName, requestDataInputDto.LastName);
        await _processService.InitRequestAsync(processId, requestData, cancellationToken);
    }

    public async Task<IActionResult> SearchRequest(CancellationToken cancellationToken = default)
    {
        ViewBag.Process1Id = _process1Id;
        return View();
    }

    [HttpPost]
    public async Task<JsonResult> SearchRequest(SearchParamsInputDto inputDto, JqueryDataTableParam jqueryDataTableParam, CancellationToken cancellationToken = default)
    {
        var result = await _process1Service.SearchRequestAsync(inputDto, jqueryDataTableParam, cancellationToken);
        return Json(result);
    }

    [HttpGet("Process1/RequestDetail/{requestId}")]
    public async Task<IActionResult> RequestDetail(Guid requestId, CancellationToken cancellationToken)
    {
        var output = await _process1Service.GetRequestDetailAsync(requestId, cancellationToken);
        
        var availablePaths = await _processService.GetAvailablePathsAsync(new Application.Dtos.Processes.GetAvailablePathsInputDto { RequestId = requestId }, cancellationToken);
        ViewBag.AvailablePaths = availablePaths.AvailablePaths;

        return View(output);
    }

    [HttpDelete]
    public async Task DeleteRequest(DeleteRequestInputDto inputDto, CancellationToken cancellationToken = default)
    {
        await _process1Service.DeleteRequestAsync(inputDto, cancellationToken);
    }

    [HttpPost]
    public async Task MakeProgress(MakeProgressInputDto inputDto, ProgressDataInputDto progressDataInputDto, CancellationToken cancellationToken = default)
    {
        var progressData = new ProgressData(progressDataInputDto.Note);
        await _processService.MakeProgressAsync(inputDto, progressData, cancellationToken);
    }
}
