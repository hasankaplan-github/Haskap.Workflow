using Microsoft.EntityFrameworkCore;
using Haskap.DddBase.Infra.Db.Contexts.NpgsqlDbContext;
using Haskap.DddBase.Domain.Providers;
using Haskap.Workflow.Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.Process1Aggregate;
using Haskap.Workflow.Domain.RequestAggregate;
using Haskap.Workflow.Domain.StateAggregate;

namespace Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext;
public class AppDbContext : BaseEfCoreNpgsqlDbContext, IWorkflowDbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentTenantProvider currentTenantProvider,
        IGlobalQueryFilterGenericProvider globalQueryFilterGenericProvider)
        : base(
            options,
            currentTenantProvider,
            globalQueryFilterGenericProvider)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly, type => type.Namespace!.Contains("WorkflowDbContext"));

        base.OnModelCreating(builder);
    }

    public DbSet<Process> Process { get; set; }
    public DbSet<Domain.ProcessAggregate.Path> Path { get; set; }
    public DbSet<Progress> Progress { get; set; }
    public DbSet<Request> Request { get; set; }
    public DbSet<State> State { get; set; }
    public DbSet<RequestData> Process1RequestData { get; set; }
}
