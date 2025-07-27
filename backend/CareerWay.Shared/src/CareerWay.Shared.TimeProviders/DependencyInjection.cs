using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.TimeProviders;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddMachineTimeProvider(
        this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        return services.AddSingleton<IDateTime, MachineDateTime>();
    }
}
