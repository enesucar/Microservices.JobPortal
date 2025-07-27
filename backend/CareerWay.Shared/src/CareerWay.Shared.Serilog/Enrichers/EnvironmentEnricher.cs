using Serilog.Core;
using Serilog.Events;

namespace CareerWay.Shared.Serilog.Enrichers;

public class EnvironmentEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var environment = propertyFactory.CreateProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!);
        logEvent.AddOrUpdateProperty(environment);
    }
}
