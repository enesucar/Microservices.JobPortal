using CareerWay.Shared.Core.Guards;
using CareerWay.Shared.EventBus;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.EventBus.Kafka;
using CareerWay.Shared.EventBus.SubscriptionManagers;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddKafka(
        this IServiceCollection services,
        Action<EventBusOptions> configureEventBus,
        Action<KafkaOptions> configureKafkaOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureEventBus, nameof(configureEventBus));
        Guard.Against.Null(configureKafkaOptions, nameof(configureKafkaOptions));

        return services
            .Configure(configureEventBus)
            .Configure(configureKafkaOptions)
            .AddSingleton<IEventBus, KafkaEventBus>()
            .AddSingleton<IKafkaSerializer, KafkaSerializer>()
            .AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
    }
}
