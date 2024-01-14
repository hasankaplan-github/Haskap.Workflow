using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Dtos.Processes.Process1;
public class RequestDetailOutputDto
{
    public Guid Id { get; set; }
    public RequestDataOutputDto Data { get; set; }
    public StateOutputDto CurrentState { get; set; }
    public string OwnerUserFirstName { get; set; }
    public string OwnerUserLastName { get; set; }
}
