using Core.Health;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core.Logging;

public static class LoggingInstaller
{
    public static IServiceCollection AddSerilogLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddHealthChecks()
            .AddElasticsearch(
                configuration["ElasticSearch:NodeUri"]!,
                name: "elasticsearch",
                tags: HealthConstants.ReadinessTags);

        return services;
    }
}