using FluentResults;
using FluentValidation;
using MediatR;

namespace Task.Manager.Contracts.TaskManagement.Command;

public record DeleteTaskCommand(Guid Id) : IRequest<Result<bool>>;

public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}