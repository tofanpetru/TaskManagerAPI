using Core.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ApiVersion = Asp.Versioning.ApiVersion;

namespace Task.Manager.Routing.TaskManagement;

internal sealed class TaskManagementActionsEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1.0))
            .Build();

        var group = app.MapGroup(EndpointConstants.TaskManagerBasePath)
            .WithTags(EndpointConstants.TaskActionsTag)
            .WithOpenApi()
            .WithValidationFilter()
            .WithApiVersionSet(versionSet);
    }
}