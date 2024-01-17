using Ardalis.GuardClauses;
using Haskap.Workflow.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.StateAggregate;
public class State : AggregateRoot
{
    public string DisplayName { get; private set; }
    public Guid ProcessId { get; private set; }
    public StateType StateType { get; set; }

    private State()
    {
    }

    internal State(Guid id, string displayName, Guid processId, StateType stateType)
        : base(id)
    {
        SetDisplayName(displayName);
        ProcessId = processId;
        StateType = stateType;
    }

    public void SetDisplayName(string displayName)
    {
        Guard.Against.NullOrWhiteSpace(displayName);

        DisplayName = displayName;
    }
}
