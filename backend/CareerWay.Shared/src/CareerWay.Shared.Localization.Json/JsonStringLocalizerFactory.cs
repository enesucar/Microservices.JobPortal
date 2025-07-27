using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Reflection;

namespace CareerWay.Shared.Localization.Json;

public class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly ConcurrentDictionary<string, JsonStringLocalizer> _localizerCache = [];
    private readonly ResourceManagerStringLocalizerFactory _innerFactory;
    private readonly JsonLocalizationOptions _localizationOptions;

    public JsonStringLocalizerFactory(
        ResourceManagerStringLocalizerFactory innerFactory,
        IOptions<JsonLocalizationOptions> localizationOptions)
    {
        _innerFactory = innerFactory;
        _localizationOptions = localizationOptions.Value;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        var localizationResource = _localizationOptions.Resources.FirstOrDefault(o => o.ResourceType == resourceSource);
        if (localizationResource == null)
        {
            throw new FileNotFoundException();
        }

        if (_localizerCache.TryGetValue(localizationResource.ResourceName, out JsonStringLocalizer? jsonStringLocalizer))
        {
            return jsonStringLocalizer;
        }

        var baseLocalizers = new List<IStringLocalizer>();
        foreach (var baseResourceType in localizationResource.BaseResourceTypes)
        {
            var baseLocalizer = Create(baseResourceType);
            baseLocalizers.Add(baseLocalizer);
        }

        var jsonResourceManager = CreateResourceManager(resourceSource, localizationResource);

        jsonStringLocalizer = new JsonStringLocalizer(jsonResourceManager, baseLocalizers);

        _localizerCache[localizationResource.ResourceName] = jsonStringLocalizer;

        return jsonStringLocalizer;
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return _innerFactory.Create(baseName, location);
    }

    private JsonResourceManager CreateResourceManager(Type resourceSource, LocalizationResource localizationResource)
    {
        var rootNamespaceAttribute = resourceSource.Assembly.GetCustomAttribute<RootNamespaceAttribute>();
        var rootNamespace = rootNamespaceAttribute?.RootNamespace ?? resourceSource.Assembly.GetName().Name;
        var resourcePath = rootNamespace + localizationResource.ResourcePath.Replace("/", ".");
        return new JsonResourceManager(resourcePath, localizationResource);
    }
}
