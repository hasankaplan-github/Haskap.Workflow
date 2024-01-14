using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.ProcessAggregate;
public class PathRole : Entity
{
    private PathRole() { }

    public PathRole(Guid id)
        : base(id) { }

    public Guid PathId { get; set; }
    public Guid RoleId { get; set; }
}
