using Haskap.DddBase.Domain.Providers;
using Haskap.Workflow.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Haskap.DddBase.Presentation.CustomAuthorization;

namespace Haskap.Workflow.Ui.MvcWebUi.ViewComponents.Process1;

public class ProgressWithNote : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(Guid requestId, Guid commandId, CancellationToken cancellationToken)
    {
        ViewBag.RequestId = requestId;
        ViewBag.CommandId = commandId;

        return View();
    }
}
