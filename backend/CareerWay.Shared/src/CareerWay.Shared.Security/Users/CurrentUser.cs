using CareerWay.Shared.Security.Claims;
using System.Security.Claims;

namespace CareerWay.Shared.Security.Users;

public class CurrentUser : ICurrentUser
{
    private readonly ClaimsPrincipal _claimsPrincipal;

    public Guid? UserId => Guid.Parse(FindClaim(ClaimTypes.NameIdentifier)?.Value!);

    public string? UserName => FindClaim(ClaimTypes.Name)?.Value;

    public string? PhoneNumber => FindClaim(ClaimTypes.MobilePhone)?.Value;

    public string? Email => FindClaim(ClaimTypes.Email)?.Value;

    public string[] Roles => FindClaims(ClaimTypes.Role).Select(claim => claim.Value).ToArray();

    public bool IsAuthenticated => _claimsPrincipal.Identity?.IsAuthenticated ?? false;

    public CurrentUser(ICurrentPrincipalAccessor currentPrincipalAccessor)
    {
        _claimsPrincipal = currentPrincipalAccessor.ClaimsPrincipal;
    }

    public Claim? FindClaim(string claimType)
    {
        return _claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == claimType);
    }

    public Claim[] FindClaims(string claimType)
    {
        return _claimsPrincipal.Claims.Where(c => c.Type == claimType).ToArray();
    }

    public Claim[] GetAllClaims()
    {
        return _claimsPrincipal.Claims.ToArray();
    }

    public bool IsInRole(string role)
    {
        return Roles.Any(userRole => userRole == role);
    }

    public bool IsInRoles(string[] roles)
    {
        return roles.Any(IsInRole);
    }
}
