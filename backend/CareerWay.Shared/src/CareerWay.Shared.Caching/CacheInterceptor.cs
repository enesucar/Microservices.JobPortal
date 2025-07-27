using CareerWay.Shared.DynamicProxy;
using CareerWay.Shared.Json;
using System.Reflection;

namespace CareerWay.Shared.Caching;

public class CacheInterceptor : MethodInterceptionBase
{
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyGenerator _cacheKeyGenerator;
    private readonly IJsonSerializer _jsonSerializer;

    public CacheInterceptor(
        ICacheService cacheService,
        ICacheKeyGenerator cacheKeyGenerator,
        IJsonSerializer jsonSerializer)
    {
        _cacheService = cacheService;
        _cacheKeyGenerator = cacheKeyGenerator;
        _jsonSerializer = jsonSerializer;
    }

    public override async Task InterceptAsync(IMethodInvocation methodInvocation)
    {
        var cacheAttribute = methodInvocation.MethodInvocationTarget.GetCustomAttributes<CacheAttribute>(true).FirstOrDefault();
        if (cacheAttribute == null)
        {
            await methodInvocation.ProceedAsync();
            return;
        }

        var returnType = methodInvocation.Method.ReturnType.GenericTypeArguments.FirstOrDefault();
        if (returnType == null)
        {
            return;
        }

        var methodName = cacheAttribute.CacheKey ?? $"{methodInvocation.MethodInvocationTarget.Name}";
        var cacheKey = _cacheKeyGenerator.Generate(methodName, methodInvocation.Arguments);
        var value = await _cacheService.GetAsync<string>(cacheKey);

        if (!string.IsNullOrEmpty(value))
        {
            var cacheData = await _jsonSerializer.DeserializeAsync(value, returnType);
            if (cacheData == null)
            {
                await methodInvocation.ProceedAsync();
                return;
            }
            else
            {
                methodInvocation.ReturnValue = cacheData;
                return;
            }
        }

        await methodInvocation.ProceedAsync();

        if (cacheAttribute.Expiration == null)
        {
            await _cacheService.SetAsync(cacheKey, await _jsonSerializer.SerializeAsync(methodInvocation.ReturnValue));
        }
        else
        {
            await _cacheService.SetAsync(cacheKey, await _jsonSerializer.SerializeAsync(methodInvocation.ReturnValue), TimeSpan.FromMinutes(cacheAttribute.Expiration.Value));
        }
    }
}
