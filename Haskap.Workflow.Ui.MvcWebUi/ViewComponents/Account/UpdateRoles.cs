using Haskap.DddBase.Domain.Providers;
using Haskap.Workflow.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Haskap.DddBase.Presentation.CustomAuthorization;
using Haskap.DddBase.Domain.UserAggregate;
using Haskap.DddBase.Application.Dtos.Accounts;
using Haskap.DddBase.Application.Contracts.Accounts;
using Haskap.DddBase.Application.Contracts.Roles;

namespace Haskap.Workflow.Ui.MvcWebUi.ViewComponents.Account;

public class UpdateRoles : ViewComponent
{
    private readonly IRoleService _roleService;
    private readonly IAccountService _accountService;
    private readonly ICurrentTenantProvider _currentTenantProvider;

    public UpdateRoles(
        IRoleService roleService,
        IAccountService accountService,
        ICurrentTenantProvider currentTenantProvider)
    {
        _roleService = roleService;
        _accountService = accountService;
        _currentTenantProvider = currentTenantProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid userId, CancellationToken cancellationToken)
    {
        ViewBag.UserId = userId;

        var roles = await _accountService.GetRolesAsync(new GetRolesInputDto{ UserId = userId }, cancellationToken);

        ViewBag.SelectedRoles = roles;

        return View(await _roleService.GetAllAsync(cancellationToken));
    }
}
