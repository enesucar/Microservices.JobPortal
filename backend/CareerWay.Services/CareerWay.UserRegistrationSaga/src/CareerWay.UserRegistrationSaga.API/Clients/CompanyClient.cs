using CareerWay.Shared.AspNetCore.Models;
using CareerWay.Shared.Json;
using CareerWay.UserRegistrationSaga.API.Interfaces;
using CareerWay.UserRegistrationSaga.API.Models;

namespace CareerWay.UserRegistrationSaga.API.Clients;

public class CompanyClient : ICompanyClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public CompanyClient(HttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<BaseApiResponse> Register(CreateCompanyHttpRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("companies", request);
        var jsonString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return (await _jsonSerializer.DeserializeAsync<SuccessApiResponse>(jsonString))!;
        }

        return new ErrorApiJsonResponse(jsonString)
        {
            Status = (int)response.StatusCode
        };
    }
}
