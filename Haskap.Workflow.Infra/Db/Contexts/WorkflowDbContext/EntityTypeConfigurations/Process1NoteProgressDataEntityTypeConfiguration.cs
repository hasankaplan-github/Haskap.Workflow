using Haskap.Workflow.Domain.Process1Aggregate;
using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.Shared.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.EntityTypeConfigurations;

public class Process1NoteProgressDataEntityTypeConfiguration : BaseEntityTypeConfiguration<NoteProgressData>
{
    public override void Configure(EntityTypeBuilder<NoteProgressData> builder)
    {
        base.Configure(builder);

        builder.HasOne<Progress>()
            .WithOne()
            .HasForeignKey<NoteProgressData>(x => x.ProgressId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
