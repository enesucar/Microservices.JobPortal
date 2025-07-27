using CareerWay.Shared.EventBus.Events;

namespace CareerWay.Shared.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent integrationEvent);

    Task SubscribeAsync<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    Task UnsubscribeAsync<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;
}
