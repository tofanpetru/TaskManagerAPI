using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Core.GeneralUtilities.MediatrPipeline;

public class RequestLoggerBehavior<TRequest>(ILogger<RequestLoggerBehavior<TRequest>> logger) : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger = logger;

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing request: '{Name}' '{Request}'", typeof(TRequest).Name, request);

        return Task.CompletedTask;
    }
}