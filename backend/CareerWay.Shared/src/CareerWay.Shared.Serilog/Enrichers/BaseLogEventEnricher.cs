using Serilog.Core;
using Serilog.Events;

namespace CareerWay.Shared.Serilog.Enrichers;

public class BaseLogEventEnricher : ILogEventEnricher
{
    private readonly string _propertyName;

    public BaseLogEventEnricher(string propertyName)
    {
        _propertyName = propertyName;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var (key, value) = logEvent.Properties.FirstOrDefault(x => x.Key == _propertyName);
        if (value != null)
        {
            var property = propertyFactory.CreateProperty(key, value);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}
