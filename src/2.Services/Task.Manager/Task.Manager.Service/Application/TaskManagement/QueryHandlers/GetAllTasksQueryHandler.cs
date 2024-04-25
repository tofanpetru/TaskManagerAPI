using Core.Domain.Entities.Pagination;
using Core.Pagination;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Task.Manager.Contracts.Commons.Enums;
using Task.Manager.Contracts.TaskManagement.Queries;
using Task.Manager.Service.Domain.DataConnections;
using Task.Manager.Service.Domain.DataModels;
using Task.Manager.Service.Extensions;

namespace Task.Manager.Service.Application.TaskManagement.QueryHandlers;

internal sealed class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, Result<PagedList<Contracts.Commons.Entities.Task>>>
{
    private readonly ITaskManagerContext _context;
    private readonly ILogger<GetAllTasksQueryHandler> _logger;

    public GetAllTasksQueryHandler(ITaskManagerContext context, ILogger<GetAllTasksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<PagedList<Contracts.Commons.Entities.Task>>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation(TaskManagementOperationsErrors.RetrieveAllTasksWithFiltersLog);

        IQueryable<TaskDataModel> queryable = _context.Tasks;

        if (!query.Status.HasValue)
        {
            queryable = queryable.Where(t => t.Status == query.Status);
        }

        if (query.Priority.HasValue)
        {
            queryable = queryable.Where(t => t.Priority == query.Priority.Value);
        }

        if (!string.IsNullOrEmpty(query.SortColumn))
        {
            switch (query.SortColumn)
            {
                case nameof(AllowedSortColumn.Status):
                    queryable = queryable.SortByPriority(query.SortOrder);
                    break;
                case nameof(AllowedSortColumn.CreatedOn):
                    queryable = queryable.SortByCreatedOn(query.SortOrder);
                    break;
                case nameof(AllowedSortColumn.ModifiedOn):
                    queryable = queryable.SortByModifiedOn(query.SortOrder);
                    break;
                default:
                    _logger.LogWarning(TaskManagementOperationsErrors.RetriveAllTasksNotSupportedSortColumnErrorLog, query.SortColumn);
                    break;
            }
        }

        var pagedList = await PagedListExtensions<TaskDataModel>.CreateAsync(
            queryable,
            query.Page,
            query.PageSize,
            cancellationToken);

        var resultList = pagedList.Items.Select(task => new Contracts.Commons.Entities.Task
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            CreatedOn = task.CreatedOn,
            ModifiedOn = task.ModifiedOn
        }).ToList();

        return Result.Ok(new PagedList<Contracts.Commons.Entities.Task>(
            resultList,
            pagedList.Page,
            pagedList.PageSize,
            pagedList.TotalCount));
    }
}