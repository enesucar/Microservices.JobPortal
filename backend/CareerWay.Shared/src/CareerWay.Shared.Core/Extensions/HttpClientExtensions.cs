using System.Net.Http.Headers;

namespace System.Net.Http;

public static class HttpClientExtensions
{
    public static HttpClient AddBearerToken(this HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}
