using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.GeneralUtilities.MediatrPipeline;

public static class MediatrBehaviorsInstaller
{
    public static IServiceCollection AddMediatrPipelineBehaviors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggerBehavior<>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));

        return serviceCollection;
    }
}