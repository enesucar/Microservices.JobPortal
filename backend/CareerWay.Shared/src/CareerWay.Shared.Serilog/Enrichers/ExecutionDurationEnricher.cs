using Serilog.Core;
using Serilog.Events;

namespace CareerWay.Shared.Serilog.Enrichers;

public class ExecutionDurationEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Properties.TryGetValue("RequestExecutionTime", out LogEventPropertyValue? value) &&
            value is ScalarValue scalarValue &&
            scalarValue.Value is DateTime rawValue)
        {
            var elapsedMilliseconds = (DateTime.Now - rawValue).TotalMilliseconds;
            var property = propertyFactory.CreateProperty("ExecutionDuration", elapsedMilliseconds);
            logEvent.AddOrUpdateProperty(property);
        }
    }
}
