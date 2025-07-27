using CareerWay.Shared.AspNetCore.Models;
using CareerWay.Shared.Json;
using CareerWay.UserRegistrationSaga.API.Interfaces;
using CareerWay.UserRegistrationSaga.API.Models;

namespace CareerWay.UserRegistrationSaga.API.Clients;

public class IdentityClient : IIdentityClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public IdentityClient(HttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<BaseApiResponse> Register(CreateUserHttpRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("users", request);
        var jsonString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return (await _jsonSerializer.DeserializeAsync<SuccessResponse<CreateUserHttpResponse>>(jsonString))!;
        }

        return new ErrorApiJsonResponse(jsonString)
        {
            Status = (int)response.StatusCode
        };
    }
}
