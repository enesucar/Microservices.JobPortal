using System.Security.Claims;

namespace System.Collections.Generic;

public static class ListExtensions
{
    public static List<Claim> Create(this List<Claim> claims, string type, string? value)
    {
        if (value == null)
        {
            return claims;
        }

        claims.Add(new Claim(type, value)); //TODO: Add AddIf Extension
        return claims;
    }
}
