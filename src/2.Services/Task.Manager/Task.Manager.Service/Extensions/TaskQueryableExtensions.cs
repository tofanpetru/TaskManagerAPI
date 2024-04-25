using Core.Domain.Entities.Pagination;
using Task.Manager.Service.Domain.DataModels;

namespace Task.Manager.Service.Extensions;

internal static class TaskQueryableExtensions
{
    public static IQueryable<TaskDataModel> SortByPriority(this IQueryable<TaskDataModel> source, SortOrder sortOrder)
    {
        return sortOrder switch
        {
            SortOrder.Ascending => source.OrderBy(t => t.Priority),
            SortOrder.Descending => source.OrderByDescending(t => t.Priority),
            _ => source
        };
    }

    public static IQueryable<TaskDataModel> SortByStatus(this IQueryable<TaskDataModel> source, SortOrder sortOrder)
    {
        return sortOrder switch
        {
            SortOrder.Ascending => source.OrderBy(t => t.Status),
            SortOrder.Descending => source.OrderByDescending(t => t.Status),
            _ => source
        };
    }

    public static IQueryable<TaskDataModel> SortByCreatedOn(this IQueryable<TaskDataModel> source, SortOrder sortOrder)
    {
        return sortOrder switch
        {
            SortOrder.Ascending => source.OrderBy(t => t.CreatedOn),
            SortOrder.Descending => source.OrderByDescending(t => t.CreatedOn),
            _ => source
        };
    }

    public static IQueryable<TaskDataModel> SortByModifiedOn(this IQueryable<TaskDataModel> source, SortOrder sortOrder)
    {
        return sortOrder switch
        {
            SortOrder.Ascending => source.OrderBy(t => t.ModifiedOn),
            SortOrder.Descending => source.OrderByDescending(t => t.ModifiedOn),
            _ => source
        };
    }
}