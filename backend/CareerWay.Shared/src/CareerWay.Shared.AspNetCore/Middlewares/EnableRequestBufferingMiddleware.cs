namespace CareerWay.Shared.AspNetCore.Middlewares;

public class EnableRequestBufferingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Request.EnableBuffering();
        await next(context);
    }
}
