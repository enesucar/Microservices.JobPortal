namespace CareerWay.Shared.Security.Tokens;

public class AccessTokenClaims
{
    public Guid? UserId { get; set; }

    public string? UserName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string[] Roles { get; set; } = [];
}
