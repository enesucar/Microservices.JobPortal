using Microsoft.Extensions.Localization;

namespace CareerWay.Shared.Localization;

public interface IEnumLocalizer<TResource> 
    where TResource : class
{
    LocalizedString GetString(Type enumType, object enumValue);

    LocalizedString GetString<TEnum>(object enumValue);

    IEnumerable<LocalizedString> GetAllStrings<TEnum>(bool includeParentCultures);
}
