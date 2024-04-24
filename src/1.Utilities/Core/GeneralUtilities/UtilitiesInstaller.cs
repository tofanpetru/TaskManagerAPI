using Core.GeneralUtilities.DateTimeProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Core.GeneralUtilities;

public static class UtilitiesInstaller
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider.DateTimeProvider>();
        return services;
    }
}