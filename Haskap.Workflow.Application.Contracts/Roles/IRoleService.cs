﻿using Haskap.Workflow.Application.Dtos.Common;
using Haskap.Workflow.Application.Dtos.Common.DataTable;
using Haskap.Workflow.Application.Dtos.Roles;

namespace Haskap.Workflow.Application.Contracts.Roles;
public interface IRoleService
{
    Task DeleteAsync(DeleteInputDto inputDto, CancellationToken cancellationToken);
    Task<List<RoleOutputDto>> GetAllAsync(CancellationToken cancellationToken);
    Task SaveNewAsync(SaveNewInputDto inputDto, CancellationToken cancellationToken);
    Task<JqueryDataTableResult> SearchAsync(SearchParamsInputDto inputDto, JqueryDataTableParam jqueryDataTableParam, CancellationToken cancellationToken);
    Task<RoleOutputDto> GetByIdAsync(Guid roleId, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateInputDto inputDto, CancellationToken cancellationToken);
    Task UpdatePermissionsAsync(UpdatePermissionsInputDto inputDto, CancellationToken cancellationToken);
    Task<List<PermissionOutputDto>> GetPermissionsAsync(Guid roleId, CancellationToken cancellationToken);
}
