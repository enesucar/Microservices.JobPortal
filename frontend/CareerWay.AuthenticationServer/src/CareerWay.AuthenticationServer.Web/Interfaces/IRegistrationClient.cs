using CareerWay.AuthenticationServer.Web.Models;
using CareerWay.Shared.AspNetCore.Models;

namespace CareerWay.AuthenticationServer.Web.Interfaces;

public interface IRegistrationClient
{
    Task<BaseApiResponse> Create(CreateCompanyRequest request);

    Task<BaseApiResponse> Create(CreateJobSeekerRequest request);
}
