namespace CareerWay.Shared.Json;

public interface IJsonSerializer
{
    Task<string> SerializeAsync(object value);

    Task<T?> DeserializeAsync<T>(string jsonString);

    Task<object?> DeserializeAsync(string jsonString, Type type);
}
