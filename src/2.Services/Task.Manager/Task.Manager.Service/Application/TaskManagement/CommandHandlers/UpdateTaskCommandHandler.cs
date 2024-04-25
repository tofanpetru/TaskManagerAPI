using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task.Manager.Contracts.TaskManagement.Command;
using Task.Manager.Service.Domain.DataConnections;

namespace Task.Manager.Service.Application.TaskManagement.CommandHandlers;

internal sealed class UpdateTaskCommandHandler(ITaskManagerContext context, ILogger<UpdateTaskCommandHandler> logger) : IRequestHandler<UpdateTaskCommand, Result>
{
    private readonly ITaskManagerContext _context = context;
    private readonly ILogger<UpdateTaskCommandHandler> _logger = logger;

    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {   //TODO CHECK if we need to update
            var task = await _context.Tasks.FirstOrDefaultAsync(
                t => t.Id == request.Id,
                cancellationToken);

            if (task == null)
            {
                _logger.LogWarning(
                    TaskManagementOperationsErrors.UpdateNonExistentTaskWarning,
                    request.Id);

                return Result.Fail(
                    new Error(
                        TaskManagementOperationsErrors.UpdateTaskNotFound));
            }

            _logger.LogInformation(
                TaskManagementOperationsErrors.UpdatingTaskIdLog,
                request.Id);

            task.Title = request.Title;
            task.Description = request.Description;
            task.Priority = request.Priority;
            task.Status = request.Status;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                TaskManagementOperationsErrors.UpdateTaskSuccessLog,
                task.Id);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, TaskManagementOperationsErrors.UpdateTaskErrorLog, request.Id);

            return Result.Fail(
                new Error(
                    TaskManagementOperationsErrors.UpdateTaskFailError)
                .CausedBy(ex));
        }
    }
}