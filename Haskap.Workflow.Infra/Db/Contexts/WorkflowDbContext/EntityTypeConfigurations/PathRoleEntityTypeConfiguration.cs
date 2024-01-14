using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.RoleAggregate;
using Haskap.Workflow.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.EntityTypeConfigurations;

public class PathRoleEntityTypeConfiguration : BaseEntityTypeConfiguration<PathRole>
{
    public override void Configure(EntityTypeBuilder<PathRole> builder)
    {
        base.Configure(builder);

        builder.HasOne<Domain.ProcessAggregate.Path>()
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.PathId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasIndex(x => new { x.UserId, x.RoleId });
    }
}
