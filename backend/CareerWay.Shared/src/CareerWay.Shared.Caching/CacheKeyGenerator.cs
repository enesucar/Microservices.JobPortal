using Microsoft.Extensions.Options;
using System.Text;

namespace CareerWay.Shared.Caching;

public class CacheKeyGenerator : ICacheKeyGenerator
{
    private readonly CacheOptions _cacheOptions;

    public CacheKeyGenerator(IOptions<CacheOptions> cacheOptions)
    {
        _cacheOptions = cacheOptions.Value;
    }

    public string Generate(string name, params object[] values)
    {
        string valuesHash = string.Empty;

        if (values.Length > 0)
        {
            string valuesPart = string.Concat(values);
            byte[] bytes = Encoding.UTF8.GetBytes(valuesPart);
            valuesHash = Convert.ToBase64String(bytes);
        }

        return valuesHash.IsNullOrWhiteSpace()
            ? $"{GetCacheKeyPrefix()}{name}"
            : $"{GetCacheKeyPrefix()}{name}:{valuesHash}";
    }

    private string GetCacheKeyPrefix()
    {
        return _cacheOptions.KeyPrefix.IsNullOrWhiteSpace()
            ? string.Empty
            : _cacheOptions.KeyPrefix.Append(":");
    }
}
