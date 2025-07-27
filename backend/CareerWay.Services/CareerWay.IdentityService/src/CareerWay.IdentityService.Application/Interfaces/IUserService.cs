using CareerWay.IdentityService.Application.Models;

namespace CareerWay.IdentityService.Application.Interfaces;

public interface IUserService
{
    Task<CreateUserResponse> Register(CreateUserRequest request);
}
