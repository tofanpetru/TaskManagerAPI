namespace Task.Manager.Routing;

internal static class EndpointConstants
{
    private const string BasePath = "/api/v{version:apiVersion}/";

    public const string TaskManagerBasePath = BasePath + "tasks";
    public const string TaskManagerTag = "Task Manager: ";

    public const string TaskCommandsTag = TaskManagerTag + "Task Actions";
    public const string TaskQueriesTag = TaskManagerTag + "Task Queries";
}