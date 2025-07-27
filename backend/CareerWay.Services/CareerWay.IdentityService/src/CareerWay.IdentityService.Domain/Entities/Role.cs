using Microsoft.AspNetCore.Identity;

namespace CareerWay.IdentityService.Domain.Entities;

public class Role : IdentityRole<long>
{
    public Role()
        : base()
    {
    }

    public Role(string roleName)
        : base(roleName)
    {
    }
}
