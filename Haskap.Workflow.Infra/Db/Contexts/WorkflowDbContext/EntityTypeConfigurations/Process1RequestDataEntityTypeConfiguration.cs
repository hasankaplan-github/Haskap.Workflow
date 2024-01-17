using Haskap.Workflow.Domain.Process1Aggregate;
using Haskap.Workflow.Domain.RequestAggregate;
using Haskap.Workflow.Domain.Shared.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.EntityTypeConfigurations;

public class Process1RequestDataEntityTypeConfiguration : BaseEntityTypeConfiguration<RequestData>
{
    public override void Configure(EntityTypeBuilder<RequestData> builder)
    {
        base.Configure(builder);

        builder.HasOne<Request>()
            .WithOne()
            .HasForeignKey<RequestData>(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
