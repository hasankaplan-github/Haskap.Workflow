using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.RequestAggregate;
using Haskap.DddBase.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.EntityTypeConfigurations;

public class RequestEntityTypeConfiguration : BaseEntityTypeConfiguration<Request>
{
    public override void Configure(EntityTypeBuilder<Request> builder)
    {
        base.Configure(builder);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.CurrentState)
            .WithMany()
            .HasForeignKey(x => x.CurrentStateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Progresses)
            .WithOne()
            .HasForeignKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Process>()
            .WithMany()
            .HasForeignKey(x => x.ProcessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
