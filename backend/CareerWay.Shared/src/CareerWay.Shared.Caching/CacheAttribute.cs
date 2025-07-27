using CareerWay.Shared.DynamicProxy;

namespace CareerWay.Shared.Caching;

public class CacheAttribute : BaseInterceptionAttribute
{
    public string? CacheKey { get; set; }

    public int? Expiration { get; set; }

    public CacheAttribute(string? cacheKey = null, int? expiration = null)
    {
        CacheKey = cacheKey;
        Expiration = expiration;
    }
}
