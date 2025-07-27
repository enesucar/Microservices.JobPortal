using CareerWay.Shared.AspNetCore.Models;
using CareerWay.UserRegistrationSaga.API.Models;

namespace CareerWay.UserRegistrationSaga.API.Interfaces;

public interface IIdentityClient
{
    Task<BaseApiResponse> Register(CreateUserHttpRequest request);
}
