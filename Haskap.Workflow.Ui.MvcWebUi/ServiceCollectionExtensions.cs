﻿using Haskap.DddBase.Domain.Providers;
using Haskap.DddBase.Infra.Providers;
using Haskap.Workflow.Infra.Db.Contexts.WorkflowDbContext;
using Microsoft.EntityFrameworkCore;
using Haskap.Workflow.Domain;
using Haskap.DddBase.Infra.Db.Interceptors;
using Haskap.Workflow.Ui.MvcWebUi.CustomAuthorization;
using Microsoft.AspNetCore.Authorization;
using Haskap.DddBase.Presentation.CustomAuthorization;
using Haskap.Workflow.Application.UseCaseServices.Processes.Process1;
using Haskap.Workflow.Application.Contracts.Processes.Process1;
using Haskap.Workflow.Domain.ProcessAggregate;
using Haskap.DddBase.Domain;
using Haskap.DddBase.Application.UseCaseServices;
using Haskap.DddBase.Infra.Db.Contexts.EfCoreContext;

namespace Haskap.Workflow.Ui.MvcWebUi;

public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddBaseDomainServices();

        services.AddTransient<ProcessDomainService>();
    }

    public static void AddUseCaseServices(this IServiceCollection services)
    {
        services.AddBaseServices();

        services.AddTransient<IProcess1Service, Process1Service>();
    }

    public static void AddProviders(this IServiceCollection services)
    {
        services.AddBaseProviders();

        services.AddSingleton<ILocalDateTimeProvider, LocalDateTimeProvider>();
        //services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        //services.AddSingleton<IJwtProvider, JwtProvider>();
    }

    public static void AddEfInterceptors(this IServiceCollection services)
    {
        services.AddBaseInterceptors();
    }

    public static void AddExternalServices(this IServiceCollection services)
    {
        //services.AddScoped<IOsymExamCalendarParser, OsymExamCalendarParser>();
        //services.AddScoped<AuditHistoryLogSaveChangesInterceptor<Guid?>>();
    }

    public static void AddPersistance(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddBaseDbContext(typeof(IWorkflowDbContext), typeof(AppDbContext));

        var connectionString = configurationManager.GetConnectionString("WorkflowNeonDbConnectionString");
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.UseSnakeCaseNamingConvention();
            options.AddBaseInterceptors(serviceProvider);
        });
    }    
}