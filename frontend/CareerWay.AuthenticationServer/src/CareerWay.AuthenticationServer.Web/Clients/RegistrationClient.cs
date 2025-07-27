using CareerWay.AuthenticationServer.Web.Interfaces;
using CareerWay.AuthenticationServer.Web.Models;
using CareerWay.Shared.AspNetCore.Models;
using CareerWay.Shared.Json;

namespace CareerWay.AuthenticationServer.Web.Clients;

public class RegistrationClient : IRegistrationClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public RegistrationClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    } 

    public async Task<BaseApiResponse> Create(CreateCompanyRequest request)
    {
        return await Create("companies", request);
    }

    public async Task<BaseApiResponse> Create(CreateJobSeekerRequest request)
    {
        return await Create("jobseekers", request);
    }

    private async Task<BaseApiResponse> Create(string requestUri, object request)
    {
        var response = await _httpClient.PostAsJsonAsync(requestUri, request);
        var jsonString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return (await _jsonSerializer.DeserializeAsync<SuccessResponse<CreateUserResponse>>(jsonString))!;
        }

        return new ErrorApiResponse()
        {
            Status = (int)response.StatusCode
        };
    }
}
