using Castle.DynamicProxy;
using CareerWay.Shared.DynamicProxy;
using CareerWay.Shared.DynamicProxy.CastleWindsor;
using CareerWay.Shared.Core.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCastleWindsorInterception(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        services.AddSingleton(new ProxyGenerator());
        return services;
    }

    public static IServiceCollection AddInterceptor<TInterceptor>(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        where TInterceptor : class, IMethodInterception
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(serviceLifetime, nameof(serviceLifetime));
        services.Add(new ServiceDescriptor(typeof(TInterceptor), typeof(TInterceptor), serviceLifetime));
        services.Add(new ServiceDescriptor(typeof(AsyncDeterminationInterceptor), typeof(AsyncDeterminationInterceptor<TInterceptor>), serviceLifetime));
        return services;
    }

    public static IServiceCollection AddProxiedTransient<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        Guard.Against.Null(services, nameof(services));
        services.AddTransient<TImplementation>();
        services.AddTransient(typeof(TInterface), CreateImplementation<TInterface, TImplementation>);
        return services;
    }

    public static IServiceCollection AddProxiedScoped<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        Guard.Against.Null(services, nameof(services));
        services.AddScoped<TImplementation>();
        services.AddScoped(typeof(TInterface), CreateImplementation<TInterface, TImplementation>);
        return services;
    }

    public static IServiceCollection AddProxiedSingleton<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        Guard.Against.Null(services, nameof(services));
        services.AddSingleton<TImplementation>();
        services.AddSingleton(typeof(TInterface), CreateImplementation<TInterface, TImplementation>);
        return services;
    }

    private static object CreateImplementation<TInterface, TImplementation>(IServiceProvider serviceProvider)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        Guard.Against.Null(serviceProvider, nameof(serviceProvider));
        var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
        var actual = serviceProvider.GetRequiredService<TImplementation>();
        var interceptors = serviceProvider.GetServices<AsyncDeterminationInterceptor>().ToArray();
        return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
    }
}
