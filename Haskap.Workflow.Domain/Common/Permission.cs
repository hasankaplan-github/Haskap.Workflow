﻿using Ardalis.GuardClauses;
using Haskap.Workflow.Domain.Shared.Consts;
using Haskap.DddBase.Domain;
using System.Security.Cryptography;
using System.Text;

namespace Haskap.Workflow.Domain.Common;
public class Permission : ValueObject
{
    public Guid Id { get; init; }
    public string Name { get; private set; }

    private Permission()
    {
    }

    public Permission(string permissionName)
    {
        Guard.Against.NullOrWhiteSpace(permissionName);

        Name = permissionName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
