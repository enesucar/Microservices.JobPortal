using CareerWay.Shared.AspNetCore.Middlewares;

namespace Microsoft.AspNetCore.Builder;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRequestTime(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<RequestTimeMiddleware>();
    }

    public static IApplicationBuilder EnableRequestBuffering(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<EnableRequestBufferingMiddleware>();
    }

    public static IApplicationBuilder PushSerilogProperties(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<PushSerilogPropertiesMiddleware>();
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
    }
}
