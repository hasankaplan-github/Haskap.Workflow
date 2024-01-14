using Haskap.Workflow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.Process1Aggregate;
public class ProgressData : AggregateRoot, IProgressData
{
    public Guid ProgressId { get; set; }
    public string? Note { get; set; }

    private ProgressData()
    {
    }

    public ProgressData(string? note)
    {
        Note = note;
    }
}
