using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.Json;
using CareerWay.Shared.Json.Newtonsoft;
using Newtonsoft.Json;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddNewtonsoftJsonSerializer(
        this IServiceCollection services,
        Action<JsonSerializerSettings> configureJsonSerializerSettings)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureJsonSerializerSettings, nameof(configureJsonSerializerSettings));

        return services
            .Configure(configureJsonSerializerSettings)
            .AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
    }
}
