using Azure;
using CareerWay.Shared.Json;
using CareerWay.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CareerWay.Web.Clients;

public class MediaClient : IMediaClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public MediaClient(HttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<string> Create(IFormFile formFile)
    {
        var form = new MultipartFormDataContent();
        using var stream = formFile.OpenReadStream();
        var streamContent = new StreamContent(stream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);
        form.Add(streamContent, "file", formFile.FileName);
        var response = await _httpClient.PostAsync("JobSeekerProfilePhotos", form);
        var responseText = await response.Content.ReadAsStringAsync();
        return responseText;
    }
}
