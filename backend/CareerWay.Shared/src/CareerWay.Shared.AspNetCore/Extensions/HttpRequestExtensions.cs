using Microsoft.Extensions.Primitives;
using System.Text;

namespace Microsoft.AspNetCore.Http;

public static class HttpRequestExtensions
{
    public static string? GetQueryString(this HttpRequest httpRequest)
    {
        string? queryString = httpRequest.QueryString.ToString();
        return queryString.IsNullOrWhiteSpace() ? null : queryString;
    }

    public static StringValues? GetHeader(this HttpRequest httpRequest, string key)
    {
        if (httpRequest.Headers.Keys.Count == 0)
        {
            return null;
        }

        if (!httpRequest.Headers.TryGetValue(key, out StringValues values))
        {
            return null;
        }

        return values;
    }

    public static IHeaderDictionary GetHeaders(this HttpRequest httpRequest)
    { 
        return httpRequest.Headers;
    }

    public async static Task<string?> GetBodyAsync(this HttpRequest httpRequest)
    {
        string? requestBody = null;
        httpRequest.Body.Position = 0;
        using (StreamReader reader = new StreamReader(httpRequest.Body, Encoding.UTF8, true, 1024, true))
        {
            requestBody = await reader.ReadToEndAsync();
        }
        httpRequest.Body.Position = 0;
        return requestBody == string.Empty ? null : requestBody;
    }
}
