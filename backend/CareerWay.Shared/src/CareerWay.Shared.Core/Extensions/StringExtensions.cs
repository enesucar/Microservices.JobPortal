using CareerWay.Shared.Core.Pluralization;
using System.Text;

namespace System;

public static class StringExtensions
{
    public static string ToBase64(this string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string FromBase64(this string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static Guid ToGuid(this string input)
    {
        return Guid.Parse(input);
    }

    public static string StripPrefix(this string text, string prefix)
    {
        return text.StartsWith(prefix) ? text.Substring(prefix.Length) : text;
    }

    public static string StripSuffix(this string text, string suffix)
    {
        return text.EndsWith(suffix) ? text.Substring(0, text.Length - suffix.Length) : text;
    }

    public static string CapitalizeFirstLetter(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }
        return char.ToUpper(input[0]) + input.Substring(1);
    }

    public static string Prepend(this string text, string prefix)
    {
        if (text == null)
        {
            return prefix ?? string.Empty;
        }
        return (prefix ?? string.Empty) + text;
    }

    public static string Append(this string text, string suffix)
    {
        if (text == null)
        {
            return suffix ?? string.Empty;
        }
        return text + (suffix ?? string.Empty);
    }

    public static bool IsNullOrEmpty(this string? text)
    {
        return string.IsNullOrEmpty(text);
    }

    public static bool IsNullOrWhiteSpace(this string? text)
    {
        return string.IsNullOrWhiteSpace(text);
    }

    public static string Singularize(this string word)
    {
        return PluralizationService.Singularize(word);
    }

    public static string Pluralize(this string word)
    {
        return PluralizationService.Pluralize(word);
    }

    public static string Pluralize(this string word, int count)
    {
        return PluralizationService.Pluralize(count, word);
    }

    public static bool IsPlural(this string word)
    {
        return PluralizationService.IsPlural(word);
    }

    public static bool IsSingular(this string word)
    {
        return PluralizationService.IsSingular(word);
    }
}
