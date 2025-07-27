using CareerWay.Shared.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CareerWay.Shared.EventBus.Events;

public abstract class EventBusBase : IEventBus
{
    protected readonly IEventBusSubscriptionsManager SubscriptionsManager;

    protected readonly IServiceProvider ServiceProvider;

    protected readonly EventBusOptions EventBusOptions;

    private readonly ILogger<EventBusBase> _logger;

    public EventBusBase(
        IEventBusSubscriptionsManager subscriptionsManager,
        IServiceProvider serviceProvider,
        IOptions<EventBusOptions> eventBusOptions)
    {
        SubscriptionsManager = subscriptionsManager;
        ServiceProvider = serviceProvider;
        EventBusOptions = eventBusOptions.Value;
        _logger = serviceProvider.GetRequiredService<ILogger<EventBusBase>>();
    }

    public abstract Task PublishAsync(IntegrationEvent integrationEvent);

    public abstract Task SubscribeAsync<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    public abstract Task UnsubscribeAsync<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    protected async Task ProcessEvent(string eventName, object integrationEvent)
    {
        var @event = integrationEvent as IntegrationEvent;
        if (@event != null)
        {
            _logger.LogInformation("Processing the event {EventName} with correlationId {CorrelationId}", eventName, @event.CorrelationId);
        }

        if (!SubscriptionsManager.HasSubscriptionsForEvent(eventName))
        {
            _logger.LogWarning("No subscription for the event: {EventName}", eventName);
            return;
        }

        var subscriptions = SubscriptionsManager.GetHandlersForEvent(eventName);
        using var serviceScope = ServiceProvider.CreateScope();

        foreach (var subscription in subscriptions)
        {
            var handler = serviceScope.ServiceProvider.GetRequiredService(subscription.HandlerType);
            if (handler == null)
            {
                continue;
            }

            var eventType = SubscriptionsManager.GetEventTypeByName(eventName);
            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
            await (Task)concreteType.GetMethod("HandleAsync")?.Invoke(handler, [integrationEvent])!;

            if (@event != null)
            {
                _logger.LogInformation("Processing completed the event {EventName} with correlationId {CorrelationId}", eventName, @event.CorrelationId);
            }
        }
    }

    protected virtual string ProcessEventName(string eventName)
    {
        return eventName
            .StripPrefix(EventBusOptions.EventNamePrefixToRemove)
            .StripSuffix(EventBusOptions.EventNameSuffixToRemove)
            .Prepend($"{EventBusOptions.ProjectName}.{EventBusOptions.ServiceName}.");
    }

    protected virtual string ProcessEventNamePattern(string eventName)
    {
        return eventName
            .StripPrefix(EventBusOptions.EventNamePrefixToRemove)
            .StripSuffix(EventBusOptions.EventNameSuffixToRemove)
            .Prepend($"^{EventBusOptions.ProjectName}.*.");
    }
}
