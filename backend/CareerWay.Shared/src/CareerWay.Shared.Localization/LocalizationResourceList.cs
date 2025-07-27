namespace CareerWay.Shared.Localization;

public class LocalizationResourceList : List<LocalizationResource>
{
    public LocalizationResource Add<TLocalizationResource>(string resourcePath, string resourceName, string? defaultCultureName = null)
    {
        var localizationResource = new LocalizationResource()
        {
            ResourcePath = resourcePath,
            ResourceType = typeof(TLocalizationResource),
            ResourceName = resourceName,
            DefaultCultureName = defaultCultureName
        };

        Add(localizationResource);

        return localizationResource;
    }
}
