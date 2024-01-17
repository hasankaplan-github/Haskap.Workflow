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

public class ProcessEntityTypeConfiguration : BaseEntityTypeConfiguration<Process>
{
    public override void Configure(EntityTypeBuilder<Process> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasMaxLength(ProcessConsts.MaxNameLength);

        builder.Property(x => x.Description)
            .HasMaxLength(ProcessConsts.MaxDescriptionLength);

        builder.HasMany(x => x.Paths)
            .WithOne()
            .HasForeignKey(x => x.ProcessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
