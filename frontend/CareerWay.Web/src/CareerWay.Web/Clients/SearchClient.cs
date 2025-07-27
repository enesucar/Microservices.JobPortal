using Azure.Core;
using CareerWay.Shared.Json;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Post;
using CareerWay.Web.Services;

namespace CareerWay.Web.Clients;

public class SearchClient : ISearchClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public SearchClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer,
        IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<SearchPostResponse>> Search(PostSearchRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"posts", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<List<SearchPostResponse>>(responseContent))!;
    }

    public async Task<List<SearchPostResponse>> Applications(long userId)
    {
        var response = await _httpClient.GetAsync($"applications/{userId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<List<SearchPostResponse>>(responseContent))!;
    }

    public async Task<List<SearchPostResponse>> RelatedPosts(long postId)
    {
        var response = await _httpClient.GetAsync($"relatedposts/{postId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<List<SearchPostResponse>>(responseContent))!;
    }
}
