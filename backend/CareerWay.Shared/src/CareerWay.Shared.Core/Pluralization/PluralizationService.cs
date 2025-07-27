using System.Text.RegularExpressions;

namespace CareerWay.Shared.Core.Pluralization;

public static class PluralizationService
{
    private static readonly IList<string> _unpluralizables = new List<string>
    {
        "equipment",
        "information",
        "rice",
        "money",
        "species",
        "series",
        "fish",
        "sheep",
        "deer"
    };

    private static readonly IDictionary<string, string> _pluralizations = new Dictionary<string, string>
    {
        { "person", "people" },
        { "ox", "oxen" },
        { "child", "children" },
        { "foot", "feet" },
        { "tooth", "teeth" },
        { "goose", "geese" },
        { "mouse", "mice" },
        { "(.*)fe?", "$1ves" },
        { "(.*)man$", "$1men" },
        { "(.+[aeiou]y)$", "$1s" },
        { "(.+[^aeiou])y$", "$1ies" },
        { "(.+z)$", "$1zes" },
        { "([m|l])ouse$", "$1ice" },
        { "(.+)(e|i)x$", @"$1ices"},
        { "(octop|vir)us$", "$1i"},
        { "(.+(s|x|sh|ch))$", @"$1es" },
        { "(.+)", @"$1s" }
    };

    private static readonly IDictionary<string, string> _singularizations = new Dictionary<string, string>
    {
        { "people", "person" },
        { "oxen", "ox" },
        { "children", "child" },
        { "feet", "foot" },
        { "teeth", "tooth" },
        { "geese", "goose" },
        { "mice", "mouse" },
        { "(.*)ives?", "$1ife" },
        { "(.*)ves?", "$1f" },
        { "(.*)men$", "$1man" },
        { "(.+[aeiou])ys$", "$1y" },
        { "(.+[^aeiou])ies$", "$1y" },
        { "(.+)zes$", "$1" },
        { "([m|l])ice$", "$1ouse" },
        { "matrices", @"matrix" },
        { "indices", @"index" },
        { "(.+[^aeiou])ices$","$1ice" },
        { "(.*)ices", @"$1ex" },
        { "(octop|vir)i$", "$1us" },
        { "(.+(s|x|sh|ch))es$", @"$1" },
        { "(.+)s", @"$1" }
    };

    public static string Singularize(string word)
    {
        if (_unpluralizables.Contains(word.ToLowerInvariant()))
        {
            return word;
        }

        foreach (var singularization in _singularizations)
        {
            if (Regex.IsMatch(word, singularization.Key))
            {
                return Regex.Replace(word, singularization.Key, singularization.Value);
            }
        }

        return word;
    }

    public static string Pluralize(int count, string singular)
    {
        if (count == 1)
        {
            return singular;
        }

        return Pluralize(singular);
    }

    public static string Pluralize(string singular)
    {
        if (_unpluralizables.Contains(singular))
        {
            return singular;
        }

        string plural = "";

        foreach (var pluralization in _pluralizations)
        {
            if (Regex.IsMatch(singular, pluralization.Key))
            {
                plural = Regex.Replace(singular, pluralization.Key, pluralization.Value);
                break;
            }
        }

        return plural;
    }

    public static bool IsPlural(string word)
    {
        if (_unpluralizables.Contains(word.ToLowerInvariant()))
        {
            return true;
        }

        foreach (var singularization in _singularizations)
        {
            if (Regex.IsMatch(word, singularization.Key))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsSingular(string word)
    {
        if (_unpluralizables.Contains(word.ToLowerInvariant()))
        {
            return true;
        }

        foreach (var pluralization in _pluralizations)
        {
            if (Regex.IsMatch(word, pluralization.Key))
            {
                return true;
            }
        }

        return false;
    }
}
