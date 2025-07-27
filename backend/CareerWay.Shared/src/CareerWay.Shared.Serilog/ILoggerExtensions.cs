using Serilog.Context;
using System.Runtime.CompilerServices;

namespace Microsoft.Extensions.Logging;

public static class ILoggerExtensions
{
    public static ILogger<TCategory> Here<TCategory>(
        this ILogger<TCategory> logger,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        LogContext.PushProperty("SourceMemberName", sourceMemberName);
        LogContext.PushProperty("SourceFilePath", sourceFilePath);
        LogContext.PushProperty("SourceLineNumber", sourceLineNumber);
        return logger;
    }
}
