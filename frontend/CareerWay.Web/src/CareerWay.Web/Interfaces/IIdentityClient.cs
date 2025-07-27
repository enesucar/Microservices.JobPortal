using CareerWay.Web.Models.Login;

namespace CareerWay.Web.Interfaces;

public interface IIdentityClient
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
