using StackExchange.Redis;

namespace CareerWay.Shared.Caching.Redis;

public class RedisCacheOptions
{
    public string ConnectionString { get; set; } = default!;

    public int DefaultDatabase { get; set; } = -1;
}
