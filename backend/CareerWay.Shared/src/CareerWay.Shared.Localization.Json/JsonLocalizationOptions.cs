using Microsoft.Extensions.Localization;

namespace CareerWay.Shared.Localization.Json;

public class JsonLocalizationOptions : LocalizationOptions
{
    public LocalizationResourceList Resources { get; } = [];
}
