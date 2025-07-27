using CareerWay.Shared.Json;
using System.Text;

namespace CareerWay.Shared.EventBus.Kafka;

public class KafkaSerializer : IKafkaSerializer
{
    private readonly IJsonSerializer _jsonSerializer;

    public KafkaSerializer(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public async Task<byte[]> SerializeAsync(object value)
    {
        var jsonValue = await _jsonSerializer.SerializeAsync(value);
        return Encoding.UTF8.GetBytes(jsonValue);
    }

    public async Task<object?> DeserializeAsync(byte[] value, Type type)
    {
        var jsonValue = Encoding.UTF8.GetString(value);
        return await _jsonSerializer.DeserializeAsync(jsonValue, type);
    }
}
