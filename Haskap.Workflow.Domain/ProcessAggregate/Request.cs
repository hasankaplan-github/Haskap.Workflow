using Ardalis.GuardClauses;
using Haskap.DddBase.Utilities.Guids;
using Haskap.Workflow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.ProcessAggregate;
public class Request : Entity
{
    public Guid ProcessId { get; private set; }
    public Guid? OwnerUserId { get; private set; }
    public Guid CurrentStateId { get; private set; }
    public State CurrentState { get; private set; }

    private List<Progress> _progresses = new();
    public IReadOnlyList<Progress> Progresses => _progresses.AsReadOnly();


    private Request()
    {
    }

    public Request(
        Guid id,
        Guid processId,
        Guid ownerUserId,
        State currentState)
        : base(id)
    {
        ProcessId = processId;
        OwnerUserId = ownerUserId;
        SetCurrentState(currentState);
    }

    public Guid MakeProgress(State toState, Guid pathId, Guid ownerUserId)
    {
        SetCurrentState(toState);
        
        var progressId = AddProgress(pathId, ownerUserId);

        return progressId;
    }

    private Guid AddProgress(Guid pathId, Guid ownerUserId)
    {
        var progress = new Progress(GuidGenerator.CreateSimpleGuid(), Id, pathId, ownerUserId);
        _progresses.Add(progress);

        return progress.Id;
    }

    private void SetCurrentState(State currentState)
    {
        Guard.Against.Null(currentState);

        CurrentState = currentState;
        CurrentStateId = currentState.Id;
    }
}
