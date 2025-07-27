using CareerWay.Shared.AspNetCore.HttpFeatures;
using CareerWay.Shared.CorrelationId;
using Serilog.Context;
using System.Net;
using System.Security.Claims;

namespace CareerWay.Shared.AspNetCore.Middlewares;

public class PushSerilogPropertiesMiddleware : IMiddleware
{
    private readonly ICorrelationId _correlationId;

    public PushSerilogPropertiesMiddleware(ICorrelationId correlationId)
    {
        _correlationId = correlationId;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var httpRequestTimeFeature = context.Features.Get<IHttpRequestTimeFeature>();

        LogContext.PushProperty("Host", context.Request.Host.ToString());
        LogContext.PushProperty("UserId", context.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value.ToGuid() ?? null);
        LogContext.PushProperty("ClientIPAddress", context.Request.GetHeader("X-Forwarded-For")?.FirstOrDefault() ?? context.Connection.RemoteIpAddress?.ToString());
        LogContext.PushProperty("ClientName", Dns.GetHostEntry(context.Connection.RemoteIpAddress?.ToString()!).HostName);
        LogContext.PushProperty("TraceIdentifier", context.TraceIdentifier);
        LogContext.PushProperty("CorrelationId", _correlationId.Get());
        LogContext.PushProperty("RequestQueryString", context.Request.GetQueryString());
        LogContext.PushProperty("RequestMethod", context.Request.Method);
        LogContext.PushProperty("RequestExecutionTime", httpRequestTimeFeature!.RequestDate);
        LogContext.PushProperty("RequestMethod", context.Request.Method);
        await next(context);
    }
}
