using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task.Manager.Contracts.TaskManagement.Command;

namespace Task.Manager.Service;

public static class TaskManagerServiceInstaller
{
    public static IServiceCollection AddTaskManagerService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining<CreateTaskCommandValidator>();

        return services;
    }
}