using CareerWay.Shared.TimeProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CareerWay.Shared.Security.Tokens;

public class JwtService : ITokenService
{
    private readonly AccessTokenOptions _accessTokenOptions;
    private readonly IDateTime _dateTime;

    public JwtService(
        IOptions<AccessTokenOptions> accessTokenOptions,
        IDateTime dateTime)
    {
        _accessTokenOptions = accessTokenOptions.Value;
        _dateTime = dateTime;
    }

    public AccessToken CreateAccessToken(AccessTokenClaims accessTokenClaims)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenOptions.SecurityKey));

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        DateTime expires = DateTime.Now.AddMinutes(_accessTokenOptions.Expiration);

        JwtSecurityToken accessToken = new JwtSecurityToken(
            audience: _accessTokenOptions.Audience,
            issuer: _accessTokenOptions.Issuer,
            expires: expires,
            notBefore: _dateTime.Now,
            signingCredentials: signingCredentials,
            claims: GetClaims(accessTokenClaims));

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        return new AccessToken()
        {
            Token = tokenHandler.WriteToken(accessToken),
            Expiration = expires
        };
    }

    private List<Claim> GetClaims(AccessTokenClaims accessTokenClaims)
    {
        List<Claim> claims = new List<Claim>();

        claims.Create(ClaimTypes.NameIdentifier, accessTokenClaims.UserId.ToString());
        claims.Create(ClaimTypes.Name, accessTokenClaims.UserName);
        claims.Create(ClaimTypes.Email, accessTokenClaims.Email?.ToString());
        claims.Create(ClaimTypes.MobilePhone, accessTokenClaims.PhoneNumber?.ToString());

        foreach (var role in accessTokenClaims.Roles)
        {
            claims.Create(ClaimTypes.Role, role);
        }

        return claims;
    }
}
