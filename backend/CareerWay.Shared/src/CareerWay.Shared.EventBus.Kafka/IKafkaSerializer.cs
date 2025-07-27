namespace CareerWay.Shared.EventBus.Kafka;

public interface IKafkaSerializer
{
    Task<byte[]> SerializeAsync(object value);

    Task<object?> DeserializeAsync(byte[] value, Type type);
}
