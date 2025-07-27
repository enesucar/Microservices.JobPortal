using CareerWay.Shared.Security.Claims;
using CareerWay.Shared.Security.Users;
using CareerWay.Web.Interfaces;
using System.Security.Claims;

namespace CareerWay.Web.Services;

public class User : CurrentUser, IUser
{  
    public long Id => long.Parse(FindClaim(ClaimTypes.NameIdentifier)?.Value!);

    public User(ICurrentPrincipalAccessor currentPrincipalAccessor) : base(currentPrincipalAccessor)
    {
    }
}
