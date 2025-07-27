namespace CareerWay.Shared.AspNetCore.HttpFeatures;

public interface IHttpRequestTimeFeature
{
    DateTime RequestDate { get; }
}
