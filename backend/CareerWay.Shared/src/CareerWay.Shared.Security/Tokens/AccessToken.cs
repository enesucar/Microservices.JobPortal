namespace CareerWay.Shared.Security.Tokens;

public class AccessToken
{
    public string Token { get; set; } = default!;

    public DateTime Expiration { get; set; }
}
