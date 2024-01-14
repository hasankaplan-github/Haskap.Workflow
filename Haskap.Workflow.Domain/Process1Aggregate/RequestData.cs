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

    public RequestData(Guid id, string firstName, string lastName)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
