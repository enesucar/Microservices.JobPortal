namespace CareerWay.Shared.Localization;

public class LocalizationResource
{
    public Type ResourceType { get; set; } = default!;

    public List<Type> BaseResourceTypes { get; } = [];

    public string ResourcePath { get; set; } = default!;

    public string ResourceName { get; set; } = default!;

    public string? DefaultCultureName { get; set; } = null;

    public LocalizationResource AddBaseTypes(params Type[] resourceTypes)
    {
        BaseResourceTypes.AddRange(resourceTypes);
        return this;
    }
}
