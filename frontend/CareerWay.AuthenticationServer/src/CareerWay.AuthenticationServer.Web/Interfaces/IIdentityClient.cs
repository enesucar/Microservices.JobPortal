using CareerWay.AuthenticationServer.Web.Models;

namespace CareerWay.AuthenticationServer.Web.Interfaces;

public interface IIdentityClient
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
