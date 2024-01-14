using Haskap.Workflow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.Process1Aggregate;
public class RequestData : AggregateRoot, IRequestData
{
    public Guid RequestId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }


    private RequestData()
    {
    }

    public RequestData(Guid requestId, string firstName, string lastName)
    {
        RequestId = requestId;
        FirstName = firstName;
        LastName = lastName;
    }
}
