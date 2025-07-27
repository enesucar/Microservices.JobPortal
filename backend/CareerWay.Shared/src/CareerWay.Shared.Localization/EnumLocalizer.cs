using Microsoft.Extensions.Localization;

namespace CareerWay.Shared.Localization;

public class EnumLocalizer<TResource> : IEnumLocalizer<TResource>
    where TResource : class
{
    private readonly IStringLocalizer<TResource> _stringLocalizer;

    public EnumLocalizer(IStringLocalizer<TResource> stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
    }

    public LocalizedString GetString(Type enumType, object enumValue)
    {
        var memberName = enumType.GetEnumName(enumValue)!;
        string[] keys =
        [
            $"Enum:{enumType.Name}:{memberName}",
            $"Enum:{enumType.Name}:{enumValue}",
            $"{enumType.Name}:{memberName}",
            $"{enumType.Name}:{enumValue}",
            memberName
        ];

        foreach (var key in keys)
        {
            var value = _stringLocalizer.GetString(key);
            if (!value.ResourceNotFound)
            {
                return value;
            }
        }

        return new LocalizedString(memberName, memberName, true);
    }

    public LocalizedString GetString<TEnum>(object enumValue)
    {
        return GetString(typeof(TEnum), enumValue);
    }

    public IEnumerable<LocalizedString> GetAllStrings<TEnum>(bool includeParentCultures)
    {
        var enumType = typeof(TEnum);
        var enumValues = enumType.GetEnumValues();
        foreach (var enumValue in enumValues)
        {
            yield return GetString(enumType, enumValue);
        }
    }
}
