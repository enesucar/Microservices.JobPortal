using Serilog.Core;
using Serilog.Events;

namespace CareerWay.Shared.Serilog.Enrichers;

public class RemovePropertiesEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.RemovePropertyIfPresent("RequestId");
        logEvent.RemovePropertyIfPresent("ConnectionId");

        foreach (var property in logEvent.Properties)
        {
            if (property.Value.ToString() == "null")
            {
                logEvent.RemovePropertyIfPresent(property.Key);
            }
        }
    }
}
