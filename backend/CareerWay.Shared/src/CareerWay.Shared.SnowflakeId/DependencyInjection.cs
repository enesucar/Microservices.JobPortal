using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.SnowflakeId;
using IdGen;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddSnowflakeIdGenerator(
        this IServiceCollection services,
        Action<SnowflakeIdOptions> configureSnowflakeIdOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureSnowflakeIdOptions, nameof(configureSnowflakeIdOptions));

        SnowflakeIdOptions snowflakeIdOptions = new SnowflakeIdOptions();
        var idGenerator = new IdGenerator(snowflakeIdOptions.GeneratorId, snowflakeIdOptions.IdGeneratorOptions);

        return services
            .Configure(configureSnowflakeIdOptions)
            .AddSingleton(idGenerator)
            .AddSingleton<ISnowflakeIdGenerator, SnowflakeIdGenerator>();
    }
}
