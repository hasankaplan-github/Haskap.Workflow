using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.Common;
public interface IRequestData
{
    Guid RequestId { get; set; }
}
