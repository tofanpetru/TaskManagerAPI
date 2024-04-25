using Core.Domain.Entities.Pagination;
using Core.Endpoints;
using Core.Result;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Task.Manager.Contracts.TaskManagement.Queries;
using ApiVersion = Asp.Versioning.ApiVersion;
using TaskStatus = Task.Manager.Contracts.Commons.Enums.TaskStatus;

namespace Task.Manager.Routing.TaskManagement;

internal sealed class TaskManagementQueriesEndpoints : IEndpointsDefinition
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1.0))
            .Build();

        var group = app.MapGroup(EndpointConstants.TaskManagerBasePath)
            .WithTags(EndpointConstants.TaskQueriesTag)
            .WithOpenApi()
            .WithValidationFilter()
            .WithApiVersionSet(versionSet);
        //TODO Add tmr all summaries
        group.MapGet("/{id:guid}", GetTaskByIdAsync)
             .Produces<Result<Contracts.Commons.Entities.Task>>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithName(nameof(GetTaskByIdAsync))
             .WithSummary("");

        group.MapGet("", GetAllTasksAsync)
             .Produces<Result<PagedList<Contracts.Commons.Entities.Task>>>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithName(nameof(GetAllTasksAsync))
             .WithSummary("");
    }

    private static async Task<IResult> GetTaskByIdAsync(
        [FromRoute] Guid id,
        IMediator mediator)
    {
        var operation = await mediator.Send(new GetTaskByIdQuery(id));

        return operation.IsSuccess
            ? Results.Ok(operation.Value)
            : operation.ToProblemDetails();
    }

    private static async Task<IResult> GetAllTasksAsync(
        [FromQuery] TaskStatus? status,
        [FromQuery] int? priority,
        [FromQuery] DateTime? createdOn,
        [FromQuery] DateTime? modifiedOn,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? sortColumn,
        [FromQuery] SortOrder sortOrder,
        IMediator mediator)
    {
        var operation = await mediator.Send(
            new GetAllTasksQuery(
                status,
                priority,
                createdOn,
                modifiedOn,
                page,
                pageSize,
                sortColumn,
                sortOrder));

        return operation.IsSuccess
            ? Results.Ok(operation.Value)
            : operation.ToProblemDetails();
    }
}