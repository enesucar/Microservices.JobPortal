namespace CareerWay.Shared.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);

    Task SetAsync(string key, object data);

    Task SetAsync(string key, object data, TimeSpan expiration);

    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> data);

    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> data, TimeSpan expiration);

    Task<bool> AnyAsync(string key);

    Task RemoveAsync(string key);

    Task RemoveByPatternAsync(string pattern);

    Task ClearAsync();
}
