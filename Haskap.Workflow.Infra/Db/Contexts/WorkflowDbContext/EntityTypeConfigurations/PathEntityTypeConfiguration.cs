using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.RoleAggregate;
using Haskap.Workflow.Domain.Shared.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.EntityTypeConfigurations;

public class PathEntityTypeConfiguration : BaseEntityTypeConfiguration<Domain.ProcessAggregate.Path>
{
    public override void Configure(EntityTypeBuilder<Domain.ProcessAggregate.Path> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.FromState)
            .WithMany()
            .HasForeignKey(x => x.FromStateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ToState)
           .WithMany()
           .HasForeignKey(x => x.ToStateId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Command)
           .WithMany()
           .HasForeignKey(x => x.CommandId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey(x => x.PathId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
