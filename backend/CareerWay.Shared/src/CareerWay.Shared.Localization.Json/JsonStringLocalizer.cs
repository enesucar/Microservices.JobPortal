using Microsoft.Extensions.Localization;
using System.Globalization;

namespace CareerWay.Shared.Localization.Json;

public class JsonStringLocalizer : IStringLocalizer
{
    private readonly JsonResourceManager _resourceManager;
    private readonly List<IStringLocalizer> _baseLocalizers;

    public JsonStringLocalizer(
        JsonResourceManager resourceManager,
        List<IStringLocalizer> baseLocalizers)
    {
        _resourceManager = resourceManager;
        _baseLocalizers = baseLocalizers;
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var resources = _resourceManager.GetResourceSet(CultureInfo.CurrentCulture, includeParentCultures);
        if (resources == null)
        {
            yield break;
        }

        var searchedLocation = $"{_resourceManager.ResourcesPath}.{CultureInfo.CurrentCulture}.json";

        foreach (var resource in resources)
        {
            yield return new LocalizedString(resource.Key, resource.Value ?? resource.Key, resource.Value == null, searchedLocation);
        }
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = GetStringSafely(name);
            var searchedLocation = $"{_resourceManager.ResourcesPath}.{CultureInfo.CurrentCulture}.json";
            return new LocalizedString(name, value ?? name, value == null, searchedLocation);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var format = GetStringSafely(name);
            var value = string.Format(format ?? name, arguments);
            var searchedLocation = $"{_resourceManager.ResourcesPath}.{CultureInfo.CurrentCulture}.json";
            return new LocalizedString(name, value ?? name, value == null, searchedLocation);
        }
    }

    protected string? GetStringSafely(string name, CultureInfo? culture = null)
    {
        var value = culture == null
            ? _resourceManager.GetString(name)
            : _resourceManager.GetString(name, culture);

        if (value == null)
        {
            foreach (var baseLocalizer in _baseLocalizers)
            {
                var localizedString = baseLocalizer.GetString(name);
                if (!localizedString.ResourceNotFound)
                {
                    return localizedString.Value;
                }
            }
        }

        return value;
    }
}
