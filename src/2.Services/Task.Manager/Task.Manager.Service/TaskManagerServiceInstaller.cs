using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Task.Manager.Service;

public static class TaskManagerServiceInstaller
{
    public static IServiceCollection AddTaskManagerService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining<CreateTableCommandValidator>();

        services.AddMediatR(x => { x.RegisterServicesFromAssembly(typeof(CreateTableCommandHandler).Assembly); });

        return services;
    }

}