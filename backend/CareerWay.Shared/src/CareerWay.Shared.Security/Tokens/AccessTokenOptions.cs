namespace CareerWay.Shared.Security.Tokens;

public class AccessTokenOptions
{
    public string Audience { get; set; } = default!;

    public string Issuer { get; set; } = default!;

    public int Expiration { get; set; }

    public string SecurityKey { get; set; } = default!;
}
