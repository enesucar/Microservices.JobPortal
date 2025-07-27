namespace CareerWay.Shared.EventBus;

public class EventBusOptions
{
    public string ProjectName { get; set; } = string.Empty;

    public string ServiceName { get; set; } = string.Empty;

    public string EventNamePrefixToRemove { get; set; } = string.Empty;

    public string EventNameSuffixToRemove { get; set; } = "IntegrationEvent";
}
