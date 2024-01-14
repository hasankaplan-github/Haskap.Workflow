using Haskap.DddBase.Domain;
using Haskap.Workflow.Domain.Process1Aggregate;
using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.RoleAggregate;
using Haskap.Workflow.Domain.UserAggregate;
using Haskap.Workflow.Domain.ViewLevelExceptionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain;
public interface IWorkflowDbContext : IUnitOfWork
{
    EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        where TEntity : class;

    ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : class;

    void AddRange(params object[] entities);

    Task AddRangeAsync(params object[] entities);

    void AddRange(IEnumerable<object> entities);

    Task AddRangeAsync(
        IEnumerable<object> entities,
        CancellationToken cancellationToken = default);

    DbSet<Role> Role { get; set; }
    DbSet<User> User { get; set; }
    DbSet<UserRole> UserRole { get; set; }
    DbSet<ViewLevelException> ViewLevelException { get; set; }

    DbSet<Process> Process { get; set; }
    DbSet<Domain.ProcessAggregate.Path> Path { get; set; }
    DbSet<Progress> Progress { get; set; }
    DbSet<Request> Request { get; set; }
    DbSet<RequestData> Process1RequestData { get; set; }
}
