using CareerWay.Shared.Json;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.JobSeekers;
using Microsoft.AspNetCore.Authentication;

namespace CareerWay.Web.Controllers;

public class JobSeekerClient : IJobSeekerClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public JobSeekerClient(HttpClient httpClient, IJsonSerializer jsonSerializer, IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<JobSeekerDetail> GetDetail(long jobSeekerId)
    {
        var response = await _httpClient.GetAsync($"jobseekers/{jobSeekerId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<JobSeekerDetail>(responseContent))!;
    }

    public async Task CreateSchools(List<CreateJobSeekerSchoolRequest> request)
    {
        await SetToken();
        var response = await _httpClient.PostAsJsonAsync($"jobseekerschools", request);
    }

    public async Task CreateReferences(List<CreateJobSeekerReferenceRequest> request)
    {
        await SetToken();
        var response = await _httpClient.PostAsJsonAsync($"jobseekerreferences", request);
    }

    private async Task SetToken()
    {
        string accessToken = (await _contextAccessor.HttpContext!.GetTokenAsync("access_token"))!;
        _httpClient.AddBearerToken(accessToken);
    }

    public async Task Edit(EditJobSeekerRequest request)
    {
        await SetToken();
        var response = await _httpClient.PutAsJsonAsync($"jobseekers", request);
    }
}