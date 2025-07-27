using Confluent.Kafka;

namespace CareerWay.Shared.EventBus.Kafka;

public class KafkaOptions
{
    public ProducerConfig ProducerConfig { get; set; } = new ProducerConfig();

    public ConsumerConfig ConsumerConfig { get; set; } = new ConsumerConfig();

    public AdminClientConfig AdminClientConfig { get; set; } = new AdminClientConfig();
}
