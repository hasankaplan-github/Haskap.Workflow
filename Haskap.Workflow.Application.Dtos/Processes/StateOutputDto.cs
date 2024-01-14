using Haskap.Workflow.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Dtos.Processes;
public class StateOutputDto
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public Guid ProcessId { get; set; }
    public StateType StateType { get; set; }
}
