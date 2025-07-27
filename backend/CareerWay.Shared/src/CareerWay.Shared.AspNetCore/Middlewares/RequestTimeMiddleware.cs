using CareerWay.Shared.AspNetCore.HttpFeatures;
using CareerWay.Shared.TimeProviders;

namespace CareerWay.Shared.AspNetCore.Middlewares;

public class RequestTimeMiddleware : IMiddleware
{
    private readonly IDateTime _dateTime;

    public RequestTimeMiddleware(IDateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var httpRequestTimeFeature = new HttpRequestTimeFeature(_dateTime);
        context.Features.Set<IHttpRequestTimeFeature>(httpRequestTimeFeature);
        await next(context);
    }
}
