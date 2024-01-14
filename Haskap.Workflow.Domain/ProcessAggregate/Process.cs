using Ardalis.GuardClauses;
using Haskap.DddBase.Utilities.Guids;
using Haskap.Workflow.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.ProcessAggregate;
public class Process : AggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; set; }

    private List<State> _states = new();
    public IReadOnlyList<State> States => _states.AsReadOnly();

    private List<Command> _commands = new();
    public IReadOnlyList<Command> Commands => _commands.AsReadOnly();

    private List<Request> _requests = new();
    public IReadOnlyList<Request> Requests => _requests.AsReadOnly();

    private List<Domain.ProcessAggregate.Path> _paths = new();
    public IReadOnlyList<Domain.ProcessAggregate.Path> Paths => _paths.AsReadOnly();

    private Process()
    {
    }

    public Process(Guid id, string name, string? description)
        : base(id)
    {
        SetName(name);
        Description = description;
    }

    public void SetName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);

        Name = name;
    }

    public void AddState(string displayName, StateType stateType)
    {
        var state = new State(GuidGenerator.CreateSimpleGuid(), displayName, Id, stateType);
        _states.Add(state);
    }

    public void AddCommand(string displayName)
    {
        var command = new Command(GuidGenerator.CreateSimpleGuid(), Id, displayName);
        _commands.Add(command);
    }

    public void AddPath(State fromState, State toState, Command command, string? viewName)
    {
        var path = new Path(GuidGenerator.CreateSimpleGuid(), Id, fromState, toState, command, viewName);
        _paths.Add(path);
    }

    public Guid InitRequest(Guid ownerUserId) 
    {
        var startState = _states.FirstOrDefault(x => x.StateType == StateType.StartState);
        var newRequest = new Request(GuidGenerator.CreateSimpleGuid(), Id, ownerUserId, startState);
        _requests.Add(newRequest);
        return newRequest.Id;
    }
}
