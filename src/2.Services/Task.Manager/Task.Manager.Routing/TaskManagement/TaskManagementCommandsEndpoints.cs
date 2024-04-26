using Core.Endpoints;
using Core.Result;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using O9d.AspNet.FluentValidation;
using Task.Manager.Contracts.TaskManagement.Command;
using ApiVersion = Asp.Versioning.ApiVersion;

namespace Task.Manager.Routing.TaskManagement;

internal sealed class TaskManagementCommandsEndpoints : IEndpointsDefinition
{
    private static class Routes
    {
        public const string Create = "/";
        public const string CreateSummary = "Creates a new task.";

        public const string Update = "/";
        public const string UpdateSummary = "Updates an existing task.";

        public const string DeleteTask = "/{id:guid}";
        public const string DeleteTaskSummary = "Deletes a task by its ID.";
    }

    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1.0))
            .Build();

        var group = app.MapGroup(EndpointConstants.TaskManagerBasePath)
            .WithTags(EndpointConstants.TaskCommandsTag)
            .WithOpenApi()
            .WithValidationFilter()
            .WithApiVersionSet(versionSet);

        group.MapPost(Routes.Create, CreateTaskAsync)
            .Accepts<CreateTaskCommand>(MediaTypes.ApplicationJson)
            .Produces<Result<Guid>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithName(nameof(CreateTaskAsync))
            .WithSummary(Routes.CreateSummary);

        group.MapDelete(Routes.DeleteTask, DeleteTaskAsync)
            .Produces(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem()
            .WithName(nameof(DeleteTaskAsync))
            .WithSummary(Routes.DeleteTaskSummary);

        group.MapPut(Routes.Update, UpdateTaskAsync)
            .Accepts<UpdateTaskCommand>(MediaTypes.ApplicationJson)
            .Produces(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithName(nameof(UpdateTaskAsync))
            .WithSummary(Routes.UpdateSummary);
    }

    private static async Task<IResult> CreateTaskAsync(
        [Validate] CreateTaskCommand command,
        IMediator mediator)
    {
        var operation = await mediator.Send(command);

        return operation.IsSuccess
            ? Results.Created(
                "/",
                operation.Value)
            : operation.ToProblemDetails();
    }

    private static async Task<IResult> DeleteTaskAsync(
        [FromRoute] Guid id,
        IMediator mediator)
    {
        var operation = await mediator.Send(new DeleteTaskCommand(id));

        return operation.IsSuccess
            ? Results.Accepted()
            : operation.ToProblemDetails();
    }

    private static async Task<IResult> UpdateTaskAsync(
        [Validate] UpdateTaskCommand command,
        IMediator mediator)
    {
        var operation = await mediator.Send(command);

        return operation.IsSuccess
            ? Results.Accepted()
            : operation.ToProblemDetails();
    }
}