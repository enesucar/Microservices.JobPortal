using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace CareerWay.Shared.Serilog.Enrichers;

public class ApplicationNameEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var applicationName = propertyFactory.CreateProperty("ApplicationName", Assembly.GetEntryAssembly()?.GetName().Name!);
        logEvent.AddOrUpdateProperty(applicationName);
    }
}
