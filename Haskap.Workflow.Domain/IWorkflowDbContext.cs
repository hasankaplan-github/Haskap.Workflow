using Haskap.DddBase.Domain;
using Haskap.Workflow.Domain.Process1Aggregate;
using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.RoleAggregate;
using Haskap.Workflow.Domain.UserAggregate;
using Haskap.Workflow.Domain.ViewLevelExceptionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Domain;
public interface IWorkflowDbContext : IUnitOfWork
{
    DbSet<Role> Role { get; set; }
    DbSet<User> User { get; set; }
    DbSet<UserRole> UserRole { get; set; }
    DbSet<ViewLevelException> ViewLevelException { get; set; }

    DbSet<Process> Process { get; set; }
    DbSet<Domain.ProcessAggregate.Path> Path { get; set; }
    DbSet<Progress> Progress { get; set; }
    DbSet<Request> Request { get; set; }
    DbSet<Process1RequestData> Process1RequestData { get; set; }
}
