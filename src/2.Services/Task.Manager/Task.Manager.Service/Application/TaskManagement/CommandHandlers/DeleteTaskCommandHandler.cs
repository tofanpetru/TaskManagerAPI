using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task.Manager.Contracts.TaskManagement.Command;
using Task.Manager.Service.Domain.DataConnections;

namespace Task.Manager.Service.Application.TaskManagement.CommandHandlers;

internal sealed class DeleteTaskCommandHandler(ITaskManagerContext context, ILogger<DeleteTaskCommandHandler> logger)
        : IRequestHandler<DeleteTaskCommand, Result<bool>>
{
    private readonly ITaskManagerContext _context = context;
    private readonly ILogger<DeleteTaskCommandHandler> _logger = logger;

    public async Task<Result<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(
                t => t.Id == request.Id,
                cancellationToken);

            if (task == null)
            {
                _logger.LogWarning(
                    TaskManagementOperationsErrors.DeleteNonExistentTaskWarning,
                    request.Id);

                return Result.Fail(
                    new Error(
                        TaskManagementOperationsErrors.DeleteTaskNotFound));
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                TaskManagementOperationsErrors.DeleteTaskSuccessLog,
                request.Id);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                TaskManagementOperationsErrors.DeleteTaskErrorLog,
                request.Id);

            return Result.Fail(
                new Error(
                    TaskManagementOperationsErrors.DeleteTaskFailError)
                .CausedBy(ex));
        }
    }
}