using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Dtos.Processes;
public class PathOutputDto
{
    public Guid Id { get; set; }
    public Guid ProcessId { get; set; }
    public Guid FromStateId { get; private set; }
    public Guid ToStateId { get; private set; }
    public Guid CommandId { get; private set; }
    public CommandOutputDto Command { get; private set; }
    public string? ViewName { get; set; }
}
