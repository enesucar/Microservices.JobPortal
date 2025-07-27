using CareerWay.Shared.AspNetCore.Logging.Middlewares;

namespace CareerWay.Shared.AspNetCore.Logging.Extensions;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomHttpLogging(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<CustomHttpLoggingMiddleware>();
    }
}
