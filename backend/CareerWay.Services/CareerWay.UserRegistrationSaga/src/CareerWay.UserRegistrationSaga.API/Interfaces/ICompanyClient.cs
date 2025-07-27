using CareerWay.Shared.AspNetCore.Models;
using CareerWay.UserRegistrationSaga.API.Models;

namespace CareerWay.UserRegistrationSaga.API.Interfaces;

public interface ICompanyClient
{
    Task<BaseApiResponse> Register(CreateCompanyHttpRequest request);
}
