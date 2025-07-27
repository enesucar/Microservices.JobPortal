using CareerWay.Shared.Caching;
using CareerWay.Shared.Caching.Redis;
using CareerWay.Shared.Core.Guards;
using StackExchange.Redis;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddRedisCache(
        this IServiceCollection services,
        Action<CacheOptions> configureCacheOptions,
        Action<RedisCacheOptions> configureRedisCacheOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureCacheOptions, nameof(configureCacheOptions));
        Guard.Against.Null(configureRedisCacheOptions, nameof(configureRedisCacheOptions));

        RedisCacheOptions redisCacheOptions = new RedisCacheOptions();
        configureRedisCacheOptions(redisCacheOptions);

        services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisCacheOptions.ConnectionString));

        return services
            .Configure(configureCacheOptions)
            .Configure(configureRedisCacheOptions)
            .AddSingleton<ICacheKeyGenerator, CacheKeyGenerator>()
            .AddSingleton<ICacheService, RedisCacheService>();
    }
}
