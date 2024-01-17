using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.RequestAggregate;
public class Progress : Entity
{
    public Guid RequestId { get; private set; }
    public Guid PathId { get; private set; }
    public DateTime ProgressDateUtc { get; private set; }
    public Guid OwnerUserId { get; private set; }

    private Progress()
    {
    }

    public Progress(Guid id, Guid requestId, Guid pathId, Guid ownerUserId)
        : base(id)
    {
        RequestId = requestId;
        PathId = pathId;
        ProgressDateUtc = DateTime.UtcNow;
        OwnerUserId = ownerUserId;
    }
}
