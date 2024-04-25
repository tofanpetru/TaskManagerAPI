using System.Text.Json.Serialization;

namespace Core.Domain.Entities.Pagination;

public class PagedList<T>(List<T> items, int page, int pageSize, int totalCount)
{
    [JsonPropertyName("items")]
    public List<T> Items { get; } = items ?? [];

    [JsonPropertyName("page")]
    public int Page { get; } = page;

    [JsonPropertyName("pageSize")]
    public int PageSize { get; } = pageSize;

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; } = totalCount;

    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage => PageSize > 1;

    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage => Page * PageSize < TotalCount;
}