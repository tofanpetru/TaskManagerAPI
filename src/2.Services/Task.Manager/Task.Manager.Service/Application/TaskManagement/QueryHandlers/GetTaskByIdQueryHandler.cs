using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task.Manager.Contracts.TaskManagement.Queries;
using Task.Manager.Service.Domain.DataConnections;

namespace Task.Manager.Service.Application.TaskManagement.QueryHandlers;

internal sealed class GetTaskByIdQueryHandler(ITaskManagerContext context, ILogger<GetTaskByIdQueryHandler> logger)
    : IRequestHandler<GetTaskByIdQuery, Result<Contracts.Commons.Entities.Task>>
{
    private readonly ITaskManagerContext _context = context;
    private readonly ILogger<GetTaskByIdQueryHandler> _logger = logger;

    public async Task<Result<Contracts.Commons.Entities.Task>> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(TaskManagementOperationsErrors.RetrieveTaskLog, query.Id);

        try
        {
            var task = await _context.Tasks
                .Select(t => new Contracts.Commons.Entities.Task
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Status = t.Status,
                    CreatedOn = t.CreatedOn,
                    ModifiedOn = t.ModifiedOn
                })
                .FirstOrDefaultAsync(
                    t => t.Id == query.Id,
                    cancellationToken);

            if (task == null)
            {
                _logger.LogWarning(TaskManagementOperationsErrors.TaskNotFoundWarning, query.Id);

                return Result.Fail(
                    new Error(
                        TaskManagementOperationsErrors.TaskNotFound));
            }

            return Result.Ok(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                TaskManagementOperationsErrors.RetrieveTaskErrorLog,
                query.Id);

            return Result.Fail(
                new Error(
                    TaskManagementOperationsErrors.RetrieveTaskFailError)
                .CausedBy(ex));
        }
    }
}