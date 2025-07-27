using CareerWay.IdentityService.Application.Models;

namespace CareerWay.IdentityService.Application.Interfaces;

public interface ILoginService
{
    Task<LoginResponse> Login(LoginRequest request);
}
