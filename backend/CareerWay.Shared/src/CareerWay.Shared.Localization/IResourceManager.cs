using System.Globalization;

namespace CareerWay.Shared.Localization;

public interface IResourceManager
{
    string? GetString(string name);

    string? GetString(string name, CultureInfo culture);
}
