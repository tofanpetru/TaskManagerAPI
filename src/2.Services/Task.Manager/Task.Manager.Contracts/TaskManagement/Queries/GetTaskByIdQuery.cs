using FluentResults;
using FluentValidation;
using MediatR;

namespace Task.Manager.Contracts.TaskManagement.Queries;

public record GetTaskByIdQuery(Guid Id) : IRequest<Result<Commons.Entities.Task>>;

public class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
{
    public GetTaskByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}