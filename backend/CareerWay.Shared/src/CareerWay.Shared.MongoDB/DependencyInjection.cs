using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.MongoDB.Contexts;
using CareerWay.Shared.MongoDB.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddMongoDB<TDbContext>(
        this IServiceCollection services,
        Action<MongoDbOptions> configureMongoDbOptions)
        where TDbContext : MongoDbContext
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureMongoDbOptions, nameof(configureMongoDbOptions));

        return services
            .AddScoped<TDbContext>()
            .Configure(configureMongoDbOptions);
    }
}
