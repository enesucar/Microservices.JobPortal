using System.Security.Claims;

namespace CareerWay.Shared.Security.Claims;

public interface ICurrentPrincipalAccessor
{
    ClaimsPrincipal ClaimsPrincipal { get; }
}
