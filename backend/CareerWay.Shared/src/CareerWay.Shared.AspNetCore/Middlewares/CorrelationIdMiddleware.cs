using CareerWay.Shared.AspNetCore.Constants;
using CareerWay.Shared.CorrelationId;
using Microsoft.Extensions.Primitives;

namespace CareerWay.Shared.AspNetCore.Middlewares;

public class CorrelationIdMiddleware : IMiddleware
{
    private readonly ICorrelationId _correlationId;

    public CorrelationIdMiddleware(ICorrelationId correlationId)
    {
        _correlationId = correlationId;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Guid correlationId;

        if (context.Request.Headers.TryGetValue(HeaderConstants.CorrelationId, out StringValues correlationIds))
        {
            var newCorrelationId = correlationIds.SingleOrDefault(k => k != null && k.Equals(HeaderConstants.CorrelationId))?.ToGuid();
            correlationId = newCorrelationId ?? Guid.NewGuid();
        }
        else
        {
            correlationId = Guid.NewGuid();
            context.Request.Headers.Append(HeaderConstants.CorrelationId, correlationId.ToString());
        }

        _correlationId.Set(correlationId);

        context.Response.OnStarting(() =>
        {
            if (!context.Response.Headers.TryGetValue(HeaderConstants.CorrelationId, out correlationIds))
            {
                context.Response.Headers.Append(HeaderConstants.CorrelationId, correlationId.ToString());
            }

            return Task.CompletedTask;
        });

        await next.Invoke(context);
    }
}
