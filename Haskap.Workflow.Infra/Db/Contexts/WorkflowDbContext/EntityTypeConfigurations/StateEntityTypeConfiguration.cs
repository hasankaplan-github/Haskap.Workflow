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

public class StateEntityTypeConfiguration : BaseEntityTypeConfiguration<State>
{
    public override void Configure(EntityTypeBuilder<State> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.DisplayName)
            .HasMaxLength(StateConsts.MaxDisplayNameLength);

        builder.Property(x => x.StateType)
            .HasConversion<string>();
    }
}
