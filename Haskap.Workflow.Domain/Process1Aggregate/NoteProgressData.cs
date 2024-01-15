using Haskap.Workflow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain.Process1Aggregate;
public class NoteProgressData : AggregateRoot, IProgressData
{
    public Guid ProgressId { get; set; }
    public string? Note { get; set; }

    private NoteProgressData()
    {
    }

    public NoteProgressData(Guid id, string? note)
        : base(id)
    {
        Note = note;
    }
}
