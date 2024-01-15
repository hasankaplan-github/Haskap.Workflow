﻿using Haskap.Workflow.Application.Dtos.Roles;
using Haskap.Workflow.Ui.MvcWebUi.CustomAuthorization;
using Haskap.DddBase.Domain.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Haskap.Workflow.Application.Dtos.Common.DataTable;
using Haskap.Workflow.Application.Contracts.Roles;

namespace Haskap.Workflow.Ui.MvcWebUi.Controllers;

[Authorize(Permissions.Workflow.Admin)]
public class RoleController : Controller
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpGet("Index")]    
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View();
    }

    [HttpPost]
    public async Task<JsonResult> Search(SearchParamsInputDto inputDto, JqueryDataTableParam jqueryDataTableParam, CancellationToken cancellationToken = default) 
    {
        var result = await _roleService.SearchAsync(inputDto, jqueryDataTableParam, cancellationToken);
        return Json(result);
    }

    [HttpPost]
    public async Task SaveNew(SaveNewInputDto inputDto, CancellationToken cancellationToken = default)
    {
        await _roleService.SaveNewAsync(inputDto, cancellationToken);
    }

    [HttpDelete]
    public async Task Delete(DeleteInputDto inputDto, CancellationToken cancellationToken = default)
    {
        await _roleService.DeleteAsync(inputDto, cancellationToken);
    }

    [HttpGet]
    public async Task<JsonResult> GetById(Guid roleId, CancellationToken cancellationToken = default)
    {
        var output = await _roleService.GetByIdAsync(roleId, cancellationToken);

        return Json(output);
    }

    [HttpPut]
    public async Task Update(UpdateInputDto inputDto, CancellationToken cancellationToken = default)
    {
        await _roleService.UpdateAsync(inputDto, cancellationToken);
    }

    [HttpGet]
    public async Task<IActionResult> LoadUpdatePermissionsViewComponent(Guid roleId, CancellationToken cancellationToken)
    {
        return ViewComponent(typeof(ViewComponents.Role.UpdatePermissions), new { roleId });
    }

    [HttpPost]
    public async Task UpdatePermissions(UpdatePermissionsInputDto inputDto, CancellationToken cancellationToken)
    {
        await _roleService.UpdatePermissionsAsync(inputDto, cancellationToken);
    }

}

