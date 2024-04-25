using System.Collections.Immutable;
using Microsoft.OpenApi.Models;

namespace Core.Health;

public static class HealthConstants
{
    public static readonly ImmutableList<string> ReadinessTags = [ReadyTag];

    public static readonly ImmutableList<OpenApiTag> SwaggerTags = [new() { Name = "Health Checks" }];
    public const string SwaggerSummary = "Checks the health readiness status";

    public const string ReadyTag = "ready";

    public const string Healthy = "Healthy";
    public const string Unhealthy = "Unhealthy";

    public const string ReadinessCheckEndpoint = "/healthz/ready";
    public const string LivenessCheckEndpoint = "/healthz/live";
}