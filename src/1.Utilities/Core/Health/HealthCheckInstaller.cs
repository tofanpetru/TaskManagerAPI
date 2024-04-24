using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Core.Health;

public static class HealthCheckInstaller
{
    public static WebApplication UseHealthCheckPaths(this WebApplication app)
    {
        app.MapHealthChecks("/healthz/ready",
            new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("ready")
            });

        app.MapHealthChecks("/healthz/live",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        return app;
    }
}