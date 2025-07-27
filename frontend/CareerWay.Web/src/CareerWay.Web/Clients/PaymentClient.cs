using CareerWay.Shared.Json;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Payment;
using Microsoft.AspNetCore.Authentication;

namespace CareerWay.Web.Clients;

public class PaymentClient : IPaymentClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public PaymentClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer,
        IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Create(CreatePaymentRequest request)
    {
        await SetToken();
        var response = await _httpClient.PostAsJsonAsync("payments", request);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    private async Task SetToken()
    {
        string accessToken = (await _contextAccessor.HttpContext!.GetTokenAsync("access_token"))!;
        _httpClient.AddBearerToken(accessToken);
    }
}
