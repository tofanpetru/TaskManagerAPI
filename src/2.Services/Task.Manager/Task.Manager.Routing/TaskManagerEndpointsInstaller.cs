using Core.Endpoints;
using Microsoft.AspNetCore.Builder;
using Task.Manager.Routing.TaskManagement;

namespace Task.Manager.Routing;

public static class TaskManagerEndpointsInstaller
{
    public static IApplicationBuilder UseTaskManagerEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints<TaskManagementCommandsEndpoints>();

        return app;
    }
}