using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CareerWay.AuthenticationServer.Web.Interfaces;

public class CustomProfileService : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var claims = new List<Claim>();

        if (context.Client.ClientId == "company")
        {
            claims.Add(new Claim(ClaimTypes.Role, "Company"));
        }

        if (context.Client.ClientId == "jobseeker")
        {
            claims.Add(new Claim(ClaimTypes.Role, "JobSeeker"));
        }

        if (context.Client.ClientId == "admin")
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        context.AddRequestedClaims(claims);
        context.IssuedClaims.AddRange(claims);

        await Task.CompletedTask;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        await Task.CompletedTask;
    }
}
