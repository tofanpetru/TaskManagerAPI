using FluentResults;
using FluentValidation;
using MediatR;
using TaskStatus = Task.Manager.Contracts.Commons.Enums.TaskStatus;

namespace Task.Manager.Contracts.TaskManagement.Command;

public record CreateTaskCommand(
    string Title,
    string Description,
    int Priority,
    TaskStatus Status) : IRequest<Result<Guid>>;


public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty();

        RuleFor(p => p.Description)
            .NotEmpty();

        RuleFor(p => p.Priority)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Status)
            .IsInEnum();
    }
}