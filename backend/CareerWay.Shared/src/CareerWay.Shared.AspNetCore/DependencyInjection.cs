using Asp.Versioning;
using CareerWay.Shared.AspNetCore.Handlers;
using CareerWay.Shared.AspNetCore.Middlewares;
using CareerWay.Shared.AspNetCore.Security.Claims;
using CareerWay.Shared.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection EnableRequestBuffering(this IServiceCollection services)
    {
        return services.AddScoped<EnableRequestBufferingMiddleware>();
    }

    public static IServiceCollection AddRequestTime(this IServiceCollection services)
    {
        return services.AddScoped<RequestTimeMiddleware>();
    }

    public static IServiceCollection AddHttpContextCurrentPrincipalAccessor(this IServiceCollection services)
    {
        return services.AddScoped<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();
    }

    public static IServiceCollection AddPushSerilogProperties(this IServiceCollection services)
    {
        return services.AddScoped<PushSerilogPropertiesMiddleware>();
    }

    public static IServiceCollection AddCorrelationIdProvider(this IServiceCollection services)
    {
        return services.AddScoped<CorrelationIdMiddleware>();
    }

    public static IServiceCollection AddCustomExceptionHandler(this IServiceCollection services)
    {
        return services.AddExceptionHandler<CustomExceptionHandler>();
    }

    public static IServiceCollection AddCustomApiVersioning(
        this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IServiceCollection ConfigureCustomRouteOptions(this IServiceCollection services)
    {
        return services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }
}
