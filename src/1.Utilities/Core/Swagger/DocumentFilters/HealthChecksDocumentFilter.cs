using Core.Health;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Swagger.DocumentFilters;

public class HealthChecksDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathItem = new OpenApiPathItem
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Get] = new OpenApiOperation
                {
                    Summary = HealthConstants.SwaggerSummary,
                    Tags = HealthConstants.SwaggerTags,
                    Responses = new OpenApiResponses
                    {
                        [((uint)StatusCodes.Status200OK).ToString()] = new OpenApiResponse { Description = HealthConstants.Healthy },
                        [((uint)StatusCodes.Status503ServiceUnavailable).ToString()] = new OpenApiResponse { Description = HealthConstants.Unhealthy }
                    }
                }
            }
        };

        swaggerDoc.Paths.Add(HealthConstants.ReadinessCheckEndpoint, pathItem);
        swaggerDoc.Paths.Add(HealthConstants.LivenessCheckEndpoint, pathItem);
    }
}