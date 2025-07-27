using Duende.IdentityModel.Client;

namespace CareerWay.Web.Interfaces;

public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}
