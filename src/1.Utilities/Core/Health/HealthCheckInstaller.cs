using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Core.Health;

public static class HealthCheckInstaller
{
    public static WebApplication UseHealthCheckPaths(this WebApplication app)
    {
        app.MapHealthChecks(HealthConstants.ReadinessCheckEndpoint,
            new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains(HealthConstants.ReadyTag)
            });

        app.MapHealthChecks(HealthConstants.LivenessCheckEndpoint,
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        return app;
    }
}