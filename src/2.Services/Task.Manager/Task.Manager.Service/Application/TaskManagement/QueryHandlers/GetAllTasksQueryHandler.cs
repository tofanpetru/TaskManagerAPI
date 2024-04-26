using Core.Domain.Entities.Pagination;
using Core.Pagination;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task.Manager.Contracts.Commons.Enums;
using Task.Manager.Contracts.TaskManagement.Queries;
using Task.Manager.Service.Domain.DataConnections;
using Task.Manager.Service.Domain.DataModels;
using Task.Manager.Service.Extensions;

namespace Task.Manager.Service.Application.TaskManagement.QueryHandlers;

internal sealed class GetAllTasksQueryHandler(ITaskManagerContext context, ILogger<GetAllTasksQueryHandler> logger)
        : IRequestHandler<GetAllTasksQuery, Result<PagedList<Contracts.Commons.Entities.Task>>>
{
    private readonly ITaskManagerContext _context = context;
    private readonly ILogger<GetAllTasksQueryHandler> _logger = logger;

    public async Task<Result<PagedList<Contracts.Commons.Entities.Task>>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(TaskManagementOperationsErrors.RetrieveAllTasksWithFiltersLog);

        var queryable = _context.Tasks.AsNoTracking();
        if (query.Status.HasValue)
        {
            var filteredTasks = await queryable
                .Where(t => t.Status == query.Status.Value)
                .ToListAsync(cancellationToken);

            var mappedTasks = MapTasksToContracts(filteredTasks);

            return Result.Ok(new PagedList<Contracts.Commons.Entities.Task>(
                mappedTasks,
                1,
                filteredTasks.Count,
                filteredTasks.Count));
        }

        queryable = ApplyFilters(queryable, query);

        var pagedList = await PagedListExtensions<TaskDataModel>.CreateAsync(
            queryable,
            query.Page,
            query.PageSize,
            cancellationToken);

        var resultList = MapTasksToContracts(pagedList.Items);

        return Result.Ok(new PagedList<Contracts.Commons.Entities.Task>(
            resultList,
            pagedList.Page,
            pagedList.PageSize,
            pagedList.TotalCount));
    }

    private static List<Contracts.Commons.Entities.Task> MapTasksToContracts(IEnumerable<TaskDataModel> tasks)
    {
        return tasks.Select(task => new Contracts.Commons.Entities.Task
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            CreatedOn = task.CreatedOn,
            ModifiedOn = task.ModifiedOn
        }).ToList();
    }

    private IQueryable<TaskDataModel> ApplyFilters(IQueryable<TaskDataModel> queryable, GetAllTasksQuery query)
    {
        if (query.Priority != null && query.Priority.HasValue)
        {
            queryable = queryable.Where(t => t.Priority == query.Priority.Value);
        }

        if (query.SortColumn != null && query.SortColumn.HasValue)
        {
            switch (query.SortColumn)
            {
                case AllowedSortColumn.Priority:
                    queryable = queryable.SortByPriority(query.SortOrder);
                    break;
                case AllowedSortColumn.Status:
                    queryable = queryable.SortByStatus(query.SortOrder);
                    break;
                case AllowedSortColumn.CreatedOn:
                    queryable = queryable.SortByCreatedOn(query.SortOrder);
                    break;
                case AllowedSortColumn.ModifiedOn:
                    queryable = queryable.SortByModifiedOn(query.SortOrder);
                    break;
                default:
                    _logger.LogWarning(TaskManagementOperationsErrors.RetriveAllTasksNotSupportedSortColumnErrorLog, query.SortColumn);
                    break;
            }
        }

        return queryable;
    }
}