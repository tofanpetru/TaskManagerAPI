using Microsoft.AspNetCore.Routing;

namespace Core.Endpoints;

public interface IEndpointsDefinition
{
    public static abstract void ConfigureEndpoints(IEndpointRouteBuilder app);
}