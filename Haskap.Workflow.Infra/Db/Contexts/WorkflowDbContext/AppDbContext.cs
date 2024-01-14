using Microsoft.EntityFrameworkCore;
using Haskap.DddBase.Infra.Db.Contexts.NpgsqlDbContext;
using Haskap.DddBase.Domain.Providers;
using Haskap.Workflow.Domain;
using Haskap.Workflow.Domain.ViewLevelExceptionAggregate;
using Haskap.Workflow.Domain.RoleAggregate;
using Haskap.Workflow.Domain.UserAggregate;
using Haskap.Workflow.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.Process1Aggregate;

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

    public DbSet<Role> Role { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<ViewLevelException> ViewLevelException { get; set; }

    public DbSet<Process> Process { get; set; }
    public DbSet<Domain.ProcessAggregate.Path> Path { get; set; }
    public DbSet<Progress> Progress { get; set; }
    public DbSet<Request> Request { get; set; }
        
    public DbSet<RequestData> Process1RequestData { get; set; }
}
