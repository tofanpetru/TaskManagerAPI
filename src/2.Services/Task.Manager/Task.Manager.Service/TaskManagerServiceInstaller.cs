using System.Reflection;
using Core.Health;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task.Manager.Contracts.TaskManagement.Command;
using Task.Manager.Service.Domain.DataConnections;
using Task.Manager.Service.Infrastructure.Data;

namespace Task.Manager.Service;

public static class TaskManagerServiceInstaller
{
    public static IServiceCollection AddTaskManagerService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining<CreateTaskCommandValidator>();

        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<TaskManagerContext>(x =>
        {
            x.UseSqlServer(connectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                options.MigrationsHistoryTable($"__{nameof(TaskManagerContext)}");
            });
        });
        services.AddScoped<ITaskManagerContext, TaskManagerContext>();

        services.AddHealthChecks()
            .AddDbContextCheck<TaskManagerContext>(
                name: "task-manager-context",
                tags: HealthConstants.ReadinessTags);

        return services;
    }

    public static IApplicationBuilder ApplyTaskManagerMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<TaskManagerContext>();
        context?.Database.Migrate();
        return app;
    }
}