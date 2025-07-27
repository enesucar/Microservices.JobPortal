using CareerWay.Shared.Json;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Post;
using Microsoft.AspNetCore.Authentication;

namespace CareerWay.Web.Clients;

public class JobAdvertClient : IJobAdvertClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public JobAdvertClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer,
        IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<PostDetail> GetDetail(long id)
    {
        var response = await _httpClient.GetAsync($"posts/{id}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<PostDetail>(responseContent))!;
    }

    public async Task Create(CreatePostRequest request)
    {
        await SetToken();
        var response = await _httpClient.PostAsJsonAsync($"posts", request); 
    }

    public async Task<Departmants> GetDepartmants()
    {
        var response = await _httpClient.GetAsync($"departmants");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<Departmants>(responseContent))!;
    }

    public async Task<Positions> GetPositions()
    {
        var response = await _httpClient.GetAsync($"positions");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<Positions>(responseContent))!;
    }

    public async Task<Cities> GetCities()
    {
        var response = await _httpClient.GetAsync($"cities");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<Cities>(responseContent))!;
    }
     
    private async Task SetToken()
    {
        string accessToken = (await _contextAccessor.HttpContext!.GetTokenAsync("access_token"))!;
        _httpClient.AddBearerToken(accessToken);
    }

    public async Task Publish(PublishPostRequest request)
    {
        await SetToken();
        var response = await _httpClient.PostAsJsonAsync($"posts/{request.Id}/publish", request);
    }
}
