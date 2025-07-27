using CareerWay.PaymentService.API.Interfaces;
using CareerWay.Shared.Security.Claims;
using CareerWay.Shared.Security.Users;
using System.Security.Claims;

namespace CareerWay.PaymentService.API.Services;

public class User : CurrentUser, IUser
{
    public long Id => long.Parse(FindClaim(ClaimTypes.NameIdentifier)?.Value);

    public User(ICurrentPrincipalAccessor currentPrincipalAccessor) : base(currentPrincipalAccessor)
    { 
    }
}
