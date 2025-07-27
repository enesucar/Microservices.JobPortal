using CareerWay.Shared.EventBus.Events;

namespace CareerWay.Shared.EventBus.Abstractions;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }

    event EventHandler<string> OnEventRemoved;

    Type GetEventTypeByName(string eventName);

    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>()
        where T : IntegrationEvent;

    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

    string GetEventName<T>()
        where T : IntegrationEvent;

    void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    void RemoveSubscription<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;

    bool HasSubscriptionsForEvent<T>()
        where T : IntegrationEvent;

    bool HasSubscriptionsForEvent(string eventName);

    void Clear();
}
