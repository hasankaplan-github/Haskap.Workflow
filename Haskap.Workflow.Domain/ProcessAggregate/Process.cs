using Ardalis.GuardClauses;
using Haskap.DddBase.Utilities.Guids;
using Haskap.Workflow.Domain.CommandAggregate;
using Haskap.Workflow.Domain.RequestAggregate;
using Haskap.Workflow.Domain.Shared.Enums;
using Haskap.Workflow.Domain.StateAggregate;
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

    public void AddPath(State fromState, State toState, Command command, string? viewName)
    {
        var path = new Path(GuidGenerator.CreateSimpleGuid(), Id, fromState, toState, command, viewName);
        _paths.Add(path);
    }

   
}
