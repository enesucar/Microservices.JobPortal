using System.Collections.Concurrent;
using System.Globalization;
using System.Resources;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CareerWay.Shared.Localization.Json;

public class JsonResourceManager : IResourceManager
{
    private readonly ConcurrentDictionary<string, ResourceSet> _resourcesCache = [];

    public string ResourcesPath { get; }

    public LocalizationResource LocalizationResource { get; set; }

    public JsonResourceManager(string resourcesPath, LocalizationResource localizationResource)
    {
        ResourcesPath = resourcesPath;
        LocalizationResource = localizationResource;
        LoadResourceSet();
    }

    public string? GetString(string name)
    {
        return GetString(name, CultureInfo.CurrentCulture);
    }

    public string? GetString(string name, CultureInfo culture)
    {
        if (_resourcesCache.IsEmpty)
        {
            return null;
        }

        var resources = GetResourceSet(culture);
        if (resources == null)
        {
            return null;
        }

        return resources.GetValueOrDefault(name);
    }

    private void LoadResourceSet()
    {
        var assembly = LocalizationResource.ResourceType.Assembly;
        var manifestResourceNames = assembly.GetManifestResourceNames().Where(o => o.StartsWith(ResourcesPath)).ToList();

        string pattern = @"\.([a-zA-Z]+-[a-zA-Z]+)\.json$";

        foreach (var manifestResourceName in manifestResourceNames)
        {
            Match match = Regex.Match(manifestResourceName, pattern);
            if (!match.Success)
            {
                continue;
            }

            var key = match.Groups[1].Value;
            if (_resourcesCache.Any(o => o.Key == key))
            {
                continue;
            }

            using Stream stream = assembly.GetManifestResourceStream(manifestResourceName)!;
            using StreamReader reader = new StreamReader(stream);
            string jsonData = reader.ReadToEnd();

            var value = JsonSerializer.Deserialize<ResourceSet>(jsonData);
            if (value != null)
            {
                _resourcesCache.TryAdd(key, value);
            }
        }
    }

    public ResourceSet GetResourceSet(CultureInfo culture, bool tryParents = false)
    {
        if (tryParents)
        {
            CultureInfo newCulture = (CultureInfo)culture.Clone();
            ResourceSet resourceSet = new ResourceSet();

            do
            {
                if (_resourcesCache.TryGetValue(newCulture.Name, out var resources))
                {
                    foreach (var entry in resources)
                    {
                        resourceSet.TryAdd(entry.Key, entry.Value);
                    }
                }

                newCulture = newCulture.Parent;
            } while (newCulture != CultureInfo.InvariantCulture);

            return resourceSet;
        }
        else
        {
            if (_resourcesCache.TryGetValue(culture.Name, out var resourceSet))
            {
                return resourceSet;
            }

            var defaultCultureName = LocalizationResource.DefaultCultureName;
            if (defaultCultureName != null)
            {
                if (_resourcesCache.TryGetValue(defaultCultureName, out resourceSet))
                {
                    return resourceSet;
                }
            }
        }

        return [];
    }
}
