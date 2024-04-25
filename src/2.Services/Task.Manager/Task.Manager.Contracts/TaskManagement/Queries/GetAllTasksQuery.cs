using Core.Domain.Entities.Pagination;
using FluentResults;
using FluentValidation;
using MediatR;
using Task.Manager.Contracts.Commons.Enums;
using TaskStatus = Task.Manager.Contracts.Commons.Enums.TaskStatus;

namespace Task.Manager.Contracts.TaskManagement.Queries;

public record GetAllTasksQuery(
    TaskStatus? Status,
    int? Priority,
    int Page,
    int PageSize,
    AllowedSortColumn? SortColumn,
    SortOrder SortOrder) : IRequest<Result<PagedList<Commons.Entities.Task>>>;

public class GetAllTasksQueryValidator : AbstractValidator<GetAllTasksQuery>
{
    public const string DateMustNotBeInTheFuture = "Date must not be in the future.";

    public GetAllTasksQueryValidator()
    {
        When(p => p.Status != null && p.Status.HasValue, () =>
        {
            RuleFor(p => p.Status!.Value)
                .IsInEnum();
        });

        When(p => p.Priority != null && p.Priority.HasValue, () =>
        {
            RuleFor(p => p.Priority!.Value)
                .GreaterThanOrEqualTo(0);
        });

        RuleFor(p => p.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(p => p.PageSize)
            .GreaterThanOrEqualTo(1);

        When(p => p.SortColumn != null && p.SortColumn.HasValue, () =>
        {
            RuleFor(p => p.SortColumn!)
                .IsInEnum();
        });

        RuleFor(p => p.SortOrder)
            .IsInEnum();
    }
}