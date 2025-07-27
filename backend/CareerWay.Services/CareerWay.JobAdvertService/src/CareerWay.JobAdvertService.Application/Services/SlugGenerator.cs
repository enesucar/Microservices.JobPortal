using CareerWay.JobAdvertService.Application.Interfaces;
using System.Text.RegularExpressions;

namespace CareerWay.JobAdvertService.Application.Services;

public class SlugGenerator : ISlugGenerator
{
    private readonly string[][] _turkishChars = {
        ["ç", "c"],
        ["ğ", "g"],
        ["ı", "i"],
        ["ö", "o"],
        ["ş", "s"],
        ["ü", "u"],
        ["Ç", "c"],
        ["Ğ", "g"],
        ["İ", "i"],
        ["Ö", "o"],
        ["Ş", "s"],
        ["Ü", "u"]
    };

    public string Generate(string text)
    {
        foreach (var pair in _turkishChars)
        {
            text = text.Replace(pair[0], pair[1]);
        }

        text = text.ToLowerInvariant();
        text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
        text = Regex.Replace(text, @"[\s-]+", "-").Trim('-');

        return text;
    }
}
