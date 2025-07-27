using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CareerWay.Shared.Json.Newtonsoft;

public class NewtonsoftJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public NewtonsoftJsonSerializer(IOptions<JsonSerializerSettings> jsonSerializerSettings)
    {
        _jsonSerializerSettings = jsonSerializerSettings.Value;
    }

    public async Task<string> SerializeAsync(object value)
    {
        var jsonString = JsonConvert.SerializeObject(value, _jsonSerializerSettings);
        return await Task.FromResult(jsonString);
    }

    public async Task<T?> DeserializeAsync<T>(string jsonString)
    {
        var obj = JsonConvert.DeserializeObject<T?>(jsonString, _jsonSerializerSettings);
        return await Task.FromResult(obj);
    }

    public async Task<object?> DeserializeAsync(string jsonString, Type type)
    {
        var obj = JsonConvert.DeserializeObject(jsonString, type, _jsonSerializerSettings);
        return await Task.FromResult(obj);
    }
}
