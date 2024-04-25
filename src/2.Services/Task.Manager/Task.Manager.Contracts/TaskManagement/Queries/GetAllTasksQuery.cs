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
    DateTime? CreatedOn,
    DateTime? ModifiedOn,
    int Page,
    int PageSize,
    string? SortColumn,
    SortOrder SortOrder) : IRequest<Result<PagedList<Commons.Entities.Task>>>;

public class GetAllTasksQueryValidator : AbstractValidator<GetAllTasksQuery>
{
    public const string DateMustNotBeInTheFuture = "Date must not be in the future.";

    public GetAllTasksQueryValidator()
    {
        When(p => p.Status.HasValue, () =>
        {
            RuleFor(p => p.Status!.Value)
                .IsInEnum();
        });

        When(p => p.Priority.HasValue, () =>
        {
            RuleFor(p => p.Priority!.Value)
                .GreaterThanOrEqualTo(0);
        });

        When(p => p.CreatedOn.HasValue, () =>
        {
            RuleFor(p => p.CreatedOn!.Value)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(DateMustNotBeInTheFuture);
        });

        When(p => p.ModifiedOn.HasValue, () =>
        {
            RuleFor(p => p.ModifiedOn!.Value)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(DateMustNotBeInTheFuture);
        });

        RuleFor(p => p.Page)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.PageSize)
            .GreaterThanOrEqualTo(0);

        When(p => !string.IsNullOrWhiteSpace(p.SortColumn), () =>
        {
            RuleFor(p => p.SortColumn!)
                .Must(BeAValidSortColumn)
                .WithMessage("SortColumn is not a valid field name.");
        });

        RuleFor(p => p.SortOrder)
            .IsInEnum();
    }

    private bool BeAValidSortColumn(string sortColumn)
    {
        return AllowedSortColumnExtensions.GetNames()
                                          .Contains(sortColumn);
    }
}