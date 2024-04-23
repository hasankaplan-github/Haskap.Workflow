using Haskap.DddBase.Domain.Providers;
using Haskap.Workflow.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Haskap.DddBase.Presentation.CustomAuthorization;
using Haskap.DddBase.Domain.UserAggregate;
using Haskap.DddBase.Application.Dtos.Accounts;
using Haskap.DddBase.Application.Contracts.Accounts;

namespace Haskap.Workflow.Ui.MvcWebUi.ViewComponents.Account;

public class UpdatePermissions : ViewComponent
{
    private readonly IPermissionProvider _permissionProvider;
    private readonly IAccountService _accountService;

    public UpdatePermissions(
        IPermissionProvider permissionProvider,
        IAccountService accountService)
    {
        _permissionProvider = permissionProvider;
        _accountService = accountService;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid? tenantId, Guid userId, CancellationToken cancellationToken)
    {
        ViewBag.UserId = userId;

        var permissions = await _accountService.GetUserPermissionsAsync(new GetUserPermissionsInputDto { UserId = userId }, cancellationToken);

        ViewBag.SelectedPermissions = permissions;

        return View(_permissionProvider.GetAllPermissions());
    }
}
