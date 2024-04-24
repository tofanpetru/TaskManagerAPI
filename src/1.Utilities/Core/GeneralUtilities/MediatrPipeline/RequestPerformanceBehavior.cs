using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.GeneralUtilities.MediatrPipeline;

public class RequestPerformanceBehavior<TRequest, TResponse>(ILogger<RequestPerformanceBehavior<TRequest, TResponse>> logger) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        long startTimestamp = Stopwatch.GetTimestamp();

        var response = await next();

        var elapsedMilliseconds = Stopwatch.GetElapsedTime(startTimestamp).Milliseconds;

        if (elapsedMilliseconds <= 5000) return response;

        logger.LogWarning("Long Running Request: '{Name}' ({ElapsedTime} milliseconds) '{Request}'", typeof(TRequest).Name,
            elapsedMilliseconds, request);

        return response;
    }
}