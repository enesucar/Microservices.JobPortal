using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Localization.Json;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddJsonStringLocalizer(
        this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        return services
            .AddLocalization()
            .AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>()
            .AddSingleton<ResourceManagerStringLocalizerFactory>();
    }
}
