using Haskap.Workflow.Domain.ViewLevelExceptionAggregate;
using Haskap.DddBase.Domain.AuditHistoryLogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext.EntityTypeConfigurations;

public class ViewLevelExceptionEntityTypeConfiguration : BaseEntityTypeConfiguration<ViewLevelException>
{
    public override void Configure(EntityTypeBuilder<ViewLevelException> builder)
    {
        base.Configure(builder);
    }
}
