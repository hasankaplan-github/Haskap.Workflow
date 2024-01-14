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

public class ProgressEntityTypeConfiguration : BaseEntityTypeConfiguration<Progress>
{
    public override void Configure(EntityTypeBuilder<Progress> builder)
    {
        base.Configure(builder);

        builder.HasOne<Domain.ProcessAggregate.Path>()
            .WithMany()
            .HasForeignKey(x => x.PathId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
