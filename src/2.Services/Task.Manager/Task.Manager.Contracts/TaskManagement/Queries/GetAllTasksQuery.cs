using FluentResults;
using FluentValidation;
using MediatR;
using Task.Manager.Contracts.Commons.Enums;
using TaskStatus = Task.Manager.Contracts.Commons.Enums.TaskStatus;

namespace Task.Manager.Contracts.TaskManagement.Queries;

public record GetAllTasksQuery(
    TaskStatus Status,
    int? Priority,
    DateTime? CreatedOn,
    DateTime? ModifiedOn) : IRequest<Result<IEnumerable<Commons.Entities.Task>>>;

public class GetAllTasksQueryValidator : AbstractValidator<GetAllTasksQuery>
{
    public GetAllTasksQueryValidator()
    {
        RuleFor(p => p.Status)
             .IsInEnum();

        RuleFor(p => p.Priority)
            .GreaterThanOrEqualTo(0)
            .When(p => p.Priority.HasValue)
            .WithMessage("Priority must be a non-negative integer when provided.");
    }
}