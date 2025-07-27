namespace CareerWay.Shared.AspNetCore.Logging.Models;

public class LoggingOptions
{
    public LoggingLevel LoggingLevel { get; set; }

    public bool IgnoreRequestHeader { get; set; }

    public bool IgnoreRequestBody { get; set; }

    public bool IgnoreResponseHeader { get; set; }

    public bool IgnoreResponseBody { get; set; }

    public string[] ExcludedRequestHeaders { get; set; } = [];

    public string[] ExcludedResponseHeaders { get; set; } = [];

    public string[] ExcludedUrls { get; set; } = [];

    public LoggingOptions()
    {
    }

    public LoggingOptions(
        LoggingLevel logLevel = LoggingLevel.Informational,
        bool ignoreRequestHeader = false,
        bool ignoreRequestBody = false,
        bool ignoreResponseHeader = false,
        bool ignoreResponseBody = false,
        string[]? excludedRequestHeaders = null,
        string[]? excludedResponseHeaders = null,
        string[]? excludedUrls = null)
    {
        LoggingLevel = logLevel;
        IgnoreRequestHeader = ignoreRequestHeader;
        IgnoreRequestBody = ignoreRequestBody;
        IgnoreResponseHeader = ignoreResponseHeader;
        IgnoreResponseBody = ignoreResponseBody;
        ExcludedRequestHeaders = excludedRequestHeaders ?? [];
        ExcludedResponseHeaders = excludedResponseHeaders ?? [];
        ExcludedUrls = excludedUrls ?? [];
    }
}
