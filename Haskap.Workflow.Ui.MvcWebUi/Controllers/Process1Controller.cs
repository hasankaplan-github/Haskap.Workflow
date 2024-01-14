using Haskap.Workflow.Application.Contracts.Processes.Process1;
using Haskap.Workflow.Application.Dtos.Processes.Process1;
using Haskap.Workflow.Domain.Process1Aggregate;
using Microsoft.AspNetCore.Mvc;

namespace Haskap.Workflow.Ui.MvcWebUi.Controllers;
public class Process1Controller : Controller
{
    private readonly IProcess1Service _process1Service;

    public Process1Controller(IProcess1Service process1Service)
    {
        _process1Service = process1Service;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> CreateRequest(CancellationToken cancellationToken = default)
    {
        ViewBag.Process1Id = Guid.NewGuid().ToString();
        return View();
    }

    [HttpPost]
    public async Task CreateRequest(InitRequestInputDto inputDto, CancellationToken cancellationToken = default)
    {
        await _process1Service.InitRequestAsync(inputDto, cancellationToken);
    }
}
