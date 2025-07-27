using CareerWay.Shared.AspNetCore.Logging.Attributes;
using CareerWay.Shared.AspNetCore.Logging.Models;
using CareerWay.Shared.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Serilog.Context;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace CareerWay.Shared.AspNetCore.Logging.Middlewares;

public class CustomHttpLoggingMiddleware : IMiddleware
{
    private readonly ILogger<CustomHttpLoggingMiddleware> _logger;
    private readonly LoggingOptions _loggingOptions;
    private readonly IJsonSerializer _jsonSerializer;
    public CustomHttpLoggingMiddleware(
        ILogger<CustomHttpLoggingMiddleware> logger,
        IOptions<LoggingOptions> loggingOptions,
        IJsonSerializer jsonSerializer)
    {
        _logger = logger;
        _loggingOptions = loggingOptions.Value;
        _jsonSerializer = jsonSerializer;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_loggingOptions.ExcludedUrls.Any(excludedUrls => excludedUrls == context.Request.Path))
        {
            await next(context);
            return;
        }

        Stream originalBody = context.Response.Body;

        try
        {
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await next(context);

                memoryStream.Position = 0;
                string responseBody = new StreamReader(memoryStream).ReadToEnd();

                await InvokeInternalAsync(context, responseBody);

                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBody);
            }
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }

    private async Task InvokeInternalAsync(HttpContext context, string responseBody)
    {
        var loggingAttribute = context.GetEndpoint()?.Metadata.GetMetadata<LoggingAttribute>();
        var options = GetLoggingOptions(loggingAttribute);

        if (context.Response.StatusCode < (int)options.LoggingLevel)
        {
            return;
        }

        if (!options.IgnoreRequestHeader) PushHeadersProperty(context.Request.Headers, options.ExcludedRequestHeaders, "RequestHeaders");
        if (!options.IgnoreRequestBody) LogContext.PushProperty("RequestBody", await context.Request.GetBodyAsync());
        if (!options.IgnoreResponseHeader) PushHeadersProperty(context.Response.Headers, options.ExcludedResponseHeaders, "ResponseHeaders");
        if (!options.IgnoreResponseBody) LogContext.PushProperty("ResponseBody", responseBody);

        LogContext.PushProperty("StatusCode", context.Response.StatusCode);

        var logMessage = "HTTP {RequestMethod} - {RequestPath} responded {StatusCode} in {ExecutionDuration}ms ({ApplicationName})";
        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (context.Response.StatusCode >= 500)
        {
            _logger.Here().LogCritical(error, logMessage);
        }
        else if (context.Response.StatusCode >= 400)
        {
            _logger.Here().LogError(error, logMessage);
        }
        else
        {
            _logger.Here().LogInformation(logMessage);
        }
    }

    private LoggingOptions GetLoggingOptions(LoggingAttribute? loggingAttribute)
    {
        if (loggingAttribute == null)
        {
            return _loggingOptions;
        }

        return new LoggingOptions()
        {
            LoggingLevel = loggingAttribute.LoggingLevel ?? _loggingOptions.LoggingLevel,
            IgnoreRequestHeader = loggingAttribute.IgnoreRequestHeader ?? _loggingOptions.IgnoreRequestHeader,
            IgnoreRequestBody = loggingAttribute.IgnoreRequestBody ?? _loggingOptions.IgnoreRequestBody,
            IgnoreResponseHeader = loggingAttribute.IgnoreResponseHeader ?? _loggingOptions.IgnoreResponseHeader,
            IgnoreResponseBody = loggingAttribute.IgnoreResponseBody ?? _loggingOptions.IgnoreResponseBody
        };
    }

    private void PushHeadersProperty(IHeaderDictionary headerDictionary, string[] excludedHeaders, string name)
    {
        var headerEntries = GetHeaders(headerDictionary, excludedHeaders);
        var stringAsJson = _jsonSerializer.SerializeAsync(headerEntries).Result;
        LogContext.PushProperty(name, headerEntries);
    }

    private List<HeaderEntry> GetHeaders(IHeaderDictionary headers, string[] excludedHeaders)
    {
        var headerEntries = new List<HeaderEntry>();
        foreach (var header in headers)
        {
            if (excludedHeaders.Contains(header.Key, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }

            foreach (var headerValue in header.Value)
            {
                if (headerValue != null)
                {
                    headerEntries.Add(new HeaderEntry { Key = header.Key, Value = headerValue });
                }
            }
        }
        return headerEntries;
    }
}
