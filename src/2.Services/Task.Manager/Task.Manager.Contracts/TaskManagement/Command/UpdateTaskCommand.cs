using FluentResults;
using FluentValidation;
using MediatR;
using TaskStatus = Task.Manager.Contracts.Commons.Enums.TaskStatus;

namespace Task.Manager.Contracts.TaskManagement.Command;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string Description,
    int Priority,
    TaskStatus Status) : IRequest<Result>;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();

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