using AutoMapper;
using Haskap.DddBase.Utilities.Guids;
using Haskap.Workflow.Application.Contracts.Processes;
using Haskap.Workflow.Application.Contracts.Processes.Process1;
using Haskap.Workflow.Application.Dtos.Common.DataTable;
using Haskap.Workflow.Application.Dtos.Processes;
using Haskap.Workflow.Application.Dtos.Processes.Process1;
using Haskap.Workflow.Application.UseCaseServices.Processes.Process1;
using Haskap.Workflow.Domain.Process1Aggregate;
using Haskap.Workflow.Domain.ProcessAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Haskap.Workflow.Ui.MvcWebUi.Controllers;

[Authorize]
public class Process1Controller : Controller
{
    private readonly Guid _process1Id = Guid.Parse("c60f6f61-5766-41e4-b7c1-f25c09dd64b3");
    private readonly IProcess1Service _process1Service;
    private readonly ProcessDomainService _processDomainService;
    private readonly IMapper _mapper;

    public Process1Controller(
        IProcess1Service process1Service,
        ProcessDomainService processDomainService,
        IMapper mapper)
    {
        _process1Service = process1Service;
        _processDomainService = processDomainService;
        _mapper = mapper;
    }

    public async Task<IActionResult> CreateRequest(CancellationToken cancellationToken = default)
    {
        ViewBag.Process1Id = _process1Id;
        return View();
    }

    [HttpPost]
    public async Task<Guid> CreateRequest(Guid processId, RequestDataInputDto requestDataInputDto, CancellationToken cancellationToken = default)
    {
        var requestId = await _process1Service.CreateRequest(processId, requestDataInputDto, cancellationToken);

        return requestId;
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
        
        ViewBag.AvailablePaths = await _process1Service.GetAvailablePathsAsync(requestId, cancellationToken);

        return View(output);
    }

    [HttpDelete]
    public async Task DeleteRequest(DeleteRequestInputDto inputDto, CancellationToken cancellationToken = default)
    {
        await _process1Service.DeleteRequestAsync(inputDto, cancellationToken);
    }

    [HttpPost]
    public async Task MakeProgress(MakeProgressInputDto inputDto, CancellationToken cancellationToken = default)
    {
        await _process1Service.MakeProgressAsync(inputDto, cancellationToken);
    }


    /*
     * Datası farklı her bir progress için 
     * bunun gibi farklı methodlar oluşturulması gerekiyor
     * Çünkü gelen data farklı olacak
     */
    [HttpPost]
    public async Task MakeProgressWithNote(MakeProgressInputDto inputDto, NoteProgressDataInputDto progressDataInputDto, CancellationToken cancellationToken = default)
    {
        await _process1Service.MakeProgressWithNoteAsync(inputDto, progressDataInputDto, cancellationToken);
    }

    [HttpGet]
    public async Task<IActionResult> LoadProgressWithNoteViewComponent(MakeProgressInputDto inputDto, CancellationToken cancellationToken)
    {
        return ViewComponent(typeof(ViewComponents.Process1.ProgressWithNote), new { inputDto.RequestId, inputDto.CommandId });
    }
}
