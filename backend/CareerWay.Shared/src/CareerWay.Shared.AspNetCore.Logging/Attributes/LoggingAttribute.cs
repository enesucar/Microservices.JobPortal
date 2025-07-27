using CareerWay.Shared.AspNetCore.Logging.Models;

namespace CareerWay.Shared.AspNetCore.Logging.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LoggingAttribute : Attribute
{
    public LoggingLevel? LoggingLevel { get; set; }

    public bool? IgnoreRequestHeader { get; set; }

    public bool? IgnoreRequestBody { get; set; }

    public bool? IgnoreResponseHeader { get; set; }

    public bool? IgnoreResponseBody { get; set; }

    public string[]? ExcludedRequestHeaders { get; set; }

    public string[]? ExcludedResponseHeaders { get; set; }

    public LoggingAttribute(
        LoggingLevel? logLevel = null,
        bool? ignoreRequestHeader = null,
        bool? ignoreRequestBody = null,
        bool? ignoreResponseHeader = null,
        bool? ignoreResponseBody = null,
        string[]? excludedRequestHeaders = null,
        string[]? excludedResponseHeaders = null)
    {
        LoggingLevel = logLevel;
        IgnoreRequestHeader = ignoreRequestHeader;
        IgnoreRequestBody = ignoreRequestBody;
        IgnoreResponseHeader = ignoreResponseHeader;
        IgnoreResponseBody = ignoreResponseBody;
        ExcludedRequestHeaders = excludedRequestHeaders;
        ExcludedResponseHeaders = excludedResponseHeaders;
    }
}
