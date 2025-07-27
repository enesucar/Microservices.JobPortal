using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.EventBus.Events;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace CareerWay.Shared.EventBus.Kafka;

public class KafkaEventBus : EventBusBase
{
    private readonly KafkaOptions _kafkaOptions;
    private readonly IAdminClient _adminClient;
    private readonly IKafkaSerializer _kafkaSerilazer;
    private readonly ILogger<KafkaEventBus> _logger;

    public KafkaEventBus(
        IEventBusSubscriptionsManager eventBusSubscriptionsManager,
        IServiceProvider serviceProvider,
        IOptions<EventBusOptions> eventBusOptions,
        IOptions<KafkaOptions> kafkaOptions,
        IKafkaSerializer kafkaSerilazer,
        ILogger<KafkaEventBus> logger)
        : base(eventBusSubscriptionsManager, serviceProvider, eventBusOptions)
    {
        _kafkaOptions = kafkaOptions.Value;
        _adminClient = new AdminClientBuilder(_kafkaOptions.AdminClientConfig).Build();
        _kafkaSerilazer = kafkaSerilazer;
        _logger = logger;
    }

    public override async Task PublishAsync(IntegrationEvent integrationEvent)
    {
        var eventName = integrationEvent.GetType().Name;
        var topicName = ProcessEventName(eventName);
        var body = await _kafkaSerilazer.SerializeAsync(integrationEvent);
        var message = new Message<Null, byte[]> { Value = body };
        _logger.LogInformation("Publishing the event {eventName} to the {topicName} topic with {correlationId} correlationId.", eventName, topicName, integrationEvent.CorrelationId);
        var producer = new ProducerBuilder<Null, byte[]>(_kafkaOptions.ProducerConfig).Build();
        await producer.ProduceAsync(topicName, message);
    }

    public override async Task SubscribeAsync<T, TH>()
    {
        var eventName = typeof(T).Name;
        SubscriptionsManager.AddSubscription<T, TH>();
        StartConsume(eventName);
        await Task.CompletedTask;
    }

    public override async Task UnsubscribeAsync<T, TH>()
    {
        var eventName = SubscriptionsManager.GetEventName<T>();
        SubscriptionsManager.RemoveSubscription<T, TH>();
        await Task.CompletedTask;
    }

    private void StartConsume(string eventName)
    {
        Task.Factory.StartNew(async () =>
        {
            if (!CheckIfTopicExists(eventName))
            {
                _logger.LogWarning("There is not topic for the event {EventName}", eventName);
            }

            while (!CheckIfTopicExists(eventName))
            {
                await Task.Delay(TimeSpan.FromSeconds(15));
            }

            var consumer = new ConsumerBuilder<Null, byte[]>(_kafkaOptions.ConsumerConfig).Build();

            consumer.Subscribe(ProcessEventNamePattern(eventName));
            _logger.LogInformation("Starting consume for the event {EventName}", eventName);

            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();
                    if (consumeResult.IsPartitionEOF)
                    {
                        continue;
                    }
                    var eventType = SubscriptionsManager.GetEventTypeByName(eventName);
                    var value = await _kafkaSerilazer.DeserializeAsync(consumeResult.Message.Value, eventType);
                    await ProcessEvent(eventName, value!);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "An error occured processing the event {EventName}", eventName);
                }
            }
        }, TaskCreationOptions.LongRunning);
    }

    private bool CheckIfTopicExists(string eventName)
    {
        var metadata = _adminClient.GetMetadata(TimeSpan.FromSeconds(10));
        var topicsMetadata = metadata.Topics;
        var processedEventName = eventName.StripSuffix(EventBusOptions.EventNameSuffixToRemove);
        var pattern = $"{EventBusOptions.ProjectName}.*.{processedEventName}";
        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var isTopicExists = topicsMetadata.Any(topic => regex.IsMatch(topic.Topic));
        return isTopicExists;
    }
}

