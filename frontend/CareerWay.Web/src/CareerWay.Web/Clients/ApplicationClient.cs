using Azure.Core;
using CareerWay.ApplicationService.API.Models;
using CareerWay.Shared.Json;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Applications;
using CareerWay.Web.Models.Post;
using Microsoft.AspNetCore.Authentication;

namespace CareerWay.Web.Clients;

public class ApplicationClient : IApplicationClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public ApplicationClient(HttpClient httpClient, IJsonSerializer jsonSerializer, IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<ApplicantionsResponse> GetList(long postId)
    {
        var response = await _httpClient.GetAsync($"applications/{postId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<ApplicantionsResponse>(responseContent))!;
    }

    public async Task<bool> IsApplied(long postId)
    {
        await SetToken();
        var response = await _httpClient.PostAsync($"applications/{postId}/check-apply", null);
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<bool>(responseContent))!;
    }

    public async Task<int> GetCount(long postId)
    { 
        var response = await _httpClient.GetAsync($"applications/{postId}/application-count");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<int>(responseContent))!;
    }

    public async Task Apply(Application application)
    {
        await SetToken();
        await _httpClient.PostAsJsonAsync($"applications/apply", application);
    }

    public async Task Withdraw(long postId)
    {
        await SetToken();
        await _httpClient.PostAsync($"applications/{postId}/withdraw", null);
    }

    public async Task SetStatus(Application application)
    {
        await SetToken();
        await _httpClient.PatchAsJsonAsync($"applications", application);
    }

    private async Task SetToken()
    {
        string accessToken = (await _contextAccessor.HttpContext!.GetTokenAsync("access_token"))!;
        _httpClient.AddBearerToken(accessToken);
    }
}
