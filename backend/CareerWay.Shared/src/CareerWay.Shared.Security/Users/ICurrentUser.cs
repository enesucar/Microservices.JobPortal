using System.Security.Claims;

namespace CareerWay.Shared.Security.Users;

public interface ICurrentUser
{
    public Guid? UserId { get; }

    string? UserName { get; }

    string? PhoneNumber { get; }

    public string? Email { get; }

    public string[] Roles { get; }

    public bool IsAuthenticated { get; }

    Claim? FindClaim(string claimType);

    Claim[] FindClaims(string claimType);

    Claim[] GetAllClaims();

    bool IsInRole(string role);

    bool IsInRoles(string[] roles);
}
