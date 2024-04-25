namespace Task.Manager.Service.Application.TaskManagement;

internal static class TaskManagementOperationsErrors
{
    //CreateTaskCommandHandler
    public const string CreatingTaskLog = "Creating a new task with title: {Title}";
    public const string TaskCreatedSuccessLog = "Task created successfully with ID: {TaskId}";
    public const string TaskCreateErrorLog = "Error occurred while creating a new task.";
    public const string TaskCreateFailError = "Failed to create the task";

    // DeleteTaskCommandHandler
    public const string DeleteNonExistentTaskWarning = "Attempted to delete a non-existent task with ID: {TaskId}";
    public const string DeleteTaskNotFound = "Task not found";
    public const string DeleteTaskSuccessLog = "Task deleted successfully with ID: {TaskId}";
    public const string DeleteTaskErrorLog = "Error occurred while deleting task with ID: {TaskId}";
    public const string DeleteTaskFailError = "Failed to delete the task";

    // UpdateTaskCommandHandler
    public const string UpdateNonExistentTaskWarning = "Attempted to update a non-existent task with ID: {TaskId}";
    public const string UpdatingTaskIdLog = "Updating task with ID: {TaskId}";
    public const string UpdateTaskNotFound = "Task not found";
    public const string UpdateTaskAlreadyExistsLog = "Task with ID {id} already has the requested values. Skipping update";
    public const string UpdateTaskSuccessLog = "Task updated successfully with ID: {TaskId}";
    public const string UpdateTaskErrorLog = "Error occurred while updating task with ID: {TaskId}";
    public const string UpdateTaskFailError = "Failed to update the task";

    // GetTaskByIdQueryHandler
    public const string RetrieveTaskLog = "Retrieving task with ID: {TaskId}";
    public const string TaskNotFoundWarning = "Task with ID: {TaskId} not found";
    public const string TaskNotFound = "Task not found";
    public const string RetrieveTaskErrorLog = "Error occurred while retrieving task with ID: {TaskId}";
    public const string RetrieveTaskFailError = "Failed to retrieve the task";
}