using CareerWay.AuthenticationServer.Web.Interfaces;
using CareerWay.AuthenticationServer.Web.Models;
using CareerWay.Shared.Json;

namespace CareerWay.AuthenticationServer.Web.Clients;

public class IdentityClient : IIdentityClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public IdentityClient(
      HttpClient httpClient,
      IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var data = (await _jsonSerializer.DeserializeAsync<LoginResponse>(responseContent))!;
        return data;
    }
}
