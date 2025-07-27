using CareerWay.Shared.Json;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Companies;
using Microsoft.AspNetCore.WebUtilities;

namespace CareerWay.Web.Clients;

public class CompanyClient : ICompanyClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public CompanyClient(HttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<CompanyDetailResponse> GetCompanyDetail(long companyId)
    {
        var response = await _httpClient.GetAsync($"companies/{companyId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<CompanyDetailResponse>(responseContent))!;
    }

    public async Task<List<CompanyPackageResponse>> GetPackages(long companyId, bool? isUsed = null)
    {
        var query = new Dictionary<string, string>();
        if (isUsed.HasValue)
        {
            query.Add("isUsed", isUsed.Value.ToString());
        };
        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"companies/{companyId}/packages", query!));
        var responseContent = await response.Content.ReadAsStringAsync();
        return (await _jsonSerializer.DeserializeAsync<List<CompanyPackageResponse>>(responseContent))!;
    }
}
