using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Task.Manager.Contracts.TaskManagement.Command;
using Task.Manager.Service.Domain.DataConnections;
using Task.Manager.Service.Domain.DataModels;

namespace Task.Manager.Service.Application.TaskManagement.CommandHandlers;

internal sealed class CreateTaskCommandHandler(ITaskManagerContext context, ILogger<CreateTaskCommandHandler> logger)
    : IRequestHandler<CreateTaskCommand, Result<Guid>>
{
    private readonly ITaskManagerContext _context = context;
    private readonly ILogger<CreateTaskCommandHandler> _logger = logger;

    public async Task<Result<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(TaskManagementOperationsErrors.CreatingTaskLog, request.Title);

        var taskDataModel = ToTaskDataModel(request);

        try
        {
            await _context.Tasks.AddAsync(taskDataModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                TaskManagementOperationsErrors.TaskCreatedSuccessLog,
                taskDataModel.Id);

            return Result.Ok(taskDataModel.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                TaskManagementOperationsErrors.TaskCreateErrorLog);

            return Result.Fail(
                new Error(TaskManagementOperationsErrors.TaskCreateFailError)
                .CausedBy(ex));
        }
    }

    private static TaskDataModel ToTaskDataModel(CreateTaskCommand command)
    {
        return new TaskDataModel
        {
            Title = command.Title,
            Description = command.Description,
            Priority = command.Priority,
            Status = command.Status
        };
    }
}