using CareerWay.Shared.Json;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Login;

namespace CareerWay.Web.Clients;

public class IdentityClient : IIdentityClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public IdentityClient(
      HttpClient httpClient,
      IJsonSerializer jsonSerializer,
      IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var data = (await _jsonSerializer.DeserializeAsync<LoginResponse>(responseContent))!;
        data.IsSuccess = response.IsSuccessStatusCode;
        return data;
    }
}
