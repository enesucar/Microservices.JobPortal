namespace CareerWay.Shared.Caching;

public interface ICacheKeyGenerator
{
    string Generate(string name, params object[] values);
}
