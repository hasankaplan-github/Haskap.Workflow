using Haskap.DddBase.Domain;
using Haskap.Workflow.Domain.Process1Aggregate;
using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.Workflow.Domain.RequestAggregate;
using Haskap.Workflow.Domain.StateAggregate;
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
public interface IWorkflowDbContext : IBaseDbContext
{
    DbSet<ViewLevelException> ViewLevelException { get; set; }

    DbSet<Process> Process { get; set; }
    DbSet<Domain.ProcessAggregate.Path> Path { get; set; }
    DbSet<Progress> Progress { get; set; }
    DbSet<Request> Request { get; set; }
    DbSet<RequestData> Process1RequestData { get; set; }
    DbSet<State> State { get; set; }
}
