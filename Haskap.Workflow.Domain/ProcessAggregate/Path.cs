using Ardalis.GuardClauses;
using Haskap.DddBase.Utilities.Guids;
using Haskap.Workflow.Domain.CommandAggregate;
using Haskap.Workflow.Domain.RoleAggregate;
using Haskap.Workflow.Domain.StateAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.ProcessAggregate;
public class Path : Entity
{
    public Guid ProcessId { get; set; }
    public Guid FromStateId { get; private set; }
    public State FromState { get; private set; }
    public Guid ToStateId { get; private set; }
    public State ToState { get; private set; }
    public Guid CommandId { get; private set; }
    public Command Command { get; private set; }
    public string? ViewName { get; set; }

    private List<PathRole> _roles = new();
    public IReadOnlyList<PathRole> Roles => _roles.AsReadOnly();

    private Path()
    {
    }

    internal Path(Guid id, Guid processId, State fromState, State toState, Command command, string? viewName)
        : base(id)
    {
        ProcessId = processId;
        SetCommand(command);
        SetFromState(fromState);
        SetToState(toState);
        ViewName = viewName;
    }

    public void AddRole(Role role)
    {
        Guard.Against.Null(role);

        _roles.Add(new PathRole(GuidGenerator.CreateSimpleGuid()) { PathId = Id, RoleId = role.Id });
    }

    public void RemoveRole(Role role)
    {
        Guard.Against.Null(role);

        _roles.RemoveAll(x => x.RoleId == role.Id);
    }

    public void SetFromState(State fromState)
    {
        Guard.Against.Null(fromState);

        FromState = fromState;
        FromStateId = fromState.Id;
    }

    public void SetToState(State toState)
    {
        Guard.Against.Null(toState);

        ToState = toState;
        ToStateId = toState.Id;
    }

    public void SetCommand(Command command)
    {
        Guard.Against.Null(command);

        Command = command;
        CommandId = command.Id;
    }
}
