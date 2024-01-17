using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Infra.Providers;
using Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext;
using Microsoft.EntityFrameworkCore;
using Haskap.Workflow.Domain;
using Haskap.Workflow.Application.UseCaseServices.Accounts;
using Haskap.DddBase.Infra.Db.Interceptors;
using Haskap.Workflow.Ui.MvcWebUi.CustomAuthorization;
using Microsoft.AspNetCore.Authorization;
using Haskap.DddBase.Presentation.CustomAuthorization;
using Haskap.Workflow.Application.UseCaseServices.ViewLevelExceptions;
using Haskap.Workflow.Domain.UserAggregate;
using Haskap.Workflow.Application.UseCaseServices.Roles;
using Haskap.Workflow.Application.Contracts.Accounts;
using Haskap.Workflow.Application.Contracts.Roles;
using Haskap.Workflow.Application.Contracts.ViewLevelExceptions;
using Haskap.Workflow.Application.Contracts.Processes;
using Haskap.Workflow.Application.UseCaseServices.Processes;
using Haskap.Workflow.Application.UseCaseServices.Processes.Process1;
using Haskap.Workflow.Application.Contracts.Processes.Process1;
using Haskap.Workflow.Domain.ProcessAggregate;

namespace Haskap.Workflow.Ui.MvcWebUi;

public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<UserDomainService>();
        services.AddTransient<ProcessDomainService>();
    }

    public static void AddUseCaseServices(this IServiceCollection services)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IViewLevelExceptionService, ViewLevelExceptionService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IProcess1Service, Process1Service>();
    }

    public static void AddProviders(this IServiceCollection services)
    {
        services.AddSingleton<ILocalDateTimeProvider, LocalDateTimeProvider>();
        //services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        //services.AddSingleton<IJwtProvider, JwtProvider>();
    }

    public static void AddEfInterceptors(this IServiceCollection services)
    {
        services.AddScoped<AuditSaveChangesInterceptor>();
        services.AddScoped<AuditHistoryLogSaveChangesInterceptor>();
        services.AddScoped<MultiTenancySaveChangesInterceptor>();
    }

    public static void AddExternalServices(this IServiceCollection services)
    {
        //services.AddScoped<IOsymExamCalendarParser, OsymExamCalendarParser>();
        //services.AddScoped<AuditHistoryLogSaveChangesInterceptor<Guid?>>();
    }

    public static void AddPersistance(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("WorkflowConnectionString");
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.UseSnakeCaseNamingConvention();
            options.AddInterceptors(
                serviceProvider.GetRequiredService<MultiTenancySaveChangesInterceptor>(),
                serviceProvider.GetRequiredService<AuditHistoryLogSaveChangesInterceptor>(),
                serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>());
        });

        services.AddScoped<IWorkflowDbContext, AppDbContext>();
    }

    public static void AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        var recipePermissionProvider = new WorkflowPermissionProvider();
        services.AddSingleton<IPermissionProvider>(recipePermissionProvider);
        services.AddAuthorization(recipePermissionProvider.ConfigureAuthorization);
    }
}