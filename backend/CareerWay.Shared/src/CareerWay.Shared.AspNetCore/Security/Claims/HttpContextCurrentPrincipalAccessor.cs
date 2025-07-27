using CareerWay.Shared.Security.Claims;
using System.Security.Claims;

namespace CareerWay.Shared.AspNetCore.Security.Claims;

public class HttpContextCurrentPrincipalAccessor : ICurrentPrincipalAccessor
{
    public ClaimsPrincipal ClaimsPrincipal { get; }

    public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal = httpContextAccessor.HttpContext?.User ?? (Thread.CurrentPrincipal as ClaimsPrincipal)!;
    }
}
