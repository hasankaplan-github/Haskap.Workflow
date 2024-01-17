using Ardalis.GuardClauses;
using Haskap.Workflow.Domain.Shared.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.CommandAggregate;
public class Command : AggregateRoot
{
    public Guid ProcessId { get; private set; }
    public string DisplayName { get; private set; }

    private Command()
    {
    }

    internal Command(Guid id, Guid processId, string displayName)
        : base(id)
    {
        ProcessId = processId;
        SetDisplayName(displayName);
    }

    public void SetDisplayName(string displayName)
    {
        Guard.Against.NullOrWhiteSpace(displayName);
        Guard.Against.InvalidInput(displayName, nameof(displayName), x => x.Length <= CommandConsts.MaxDisplayNameLength);

        DisplayName = displayName;
    }
}
