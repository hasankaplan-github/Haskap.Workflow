﻿namespace Haskap.Workflow.Application.Dtos.Roles;

public class UpdateInputDto
{
    public Guid RoleId { get; set; }
    public string NewName { get; set; }
}