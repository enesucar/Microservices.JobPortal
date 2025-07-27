namespace CareerWay.Shared.Security.Tokens;

public interface ITokenService
{
    AccessToken CreateAccessToken(AccessTokenClaims accessTokenClaims);
}
