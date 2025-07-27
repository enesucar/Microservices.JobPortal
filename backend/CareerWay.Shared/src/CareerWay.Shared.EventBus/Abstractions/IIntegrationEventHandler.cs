using CareerWay.Shared.EventBus.Events;

namespace CareerWay.Shared.EventBus.Abstractions;

public interface IIntegrationEventHandler
{
}

public interface IIntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task HandleAsync(TIntegrationEvent integrationEvent);
}
