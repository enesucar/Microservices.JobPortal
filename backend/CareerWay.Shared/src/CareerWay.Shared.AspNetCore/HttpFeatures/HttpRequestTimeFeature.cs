using CareerWay.Shared.TimeProviders;

namespace CareerWay.Shared.AspNetCore.HttpFeatures;

public class HttpRequestTimeFeature : IHttpRequestTimeFeature
{
    public DateTime RequestDate { get; }

    public HttpRequestTimeFeature(IDateTime dateTime)
    {
        RequestDate = dateTime.Now;
    }
}
