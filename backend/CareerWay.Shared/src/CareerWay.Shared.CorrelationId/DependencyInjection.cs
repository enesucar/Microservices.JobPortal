using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.CorrelationId;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomCorrelationId(
        this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        return services.AddScoped<ICorrelationId, CustomCorrelationId>();
    }
}
