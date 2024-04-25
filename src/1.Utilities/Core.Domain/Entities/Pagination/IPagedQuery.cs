namespace Core.Domain.Entities.Pagination;

public interface IPagedQuery
{
    int Page { get; set; }

    int PageSize { get; set; }
}