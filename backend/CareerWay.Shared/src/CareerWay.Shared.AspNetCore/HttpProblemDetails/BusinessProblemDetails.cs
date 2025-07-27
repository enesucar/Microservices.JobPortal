using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Shared.AspNetCore.HttpProblemDetails;

public class BusinessProblemDetails : ProblemDetails
{
    public BusinessProblemDetails(string? detail, string? instance, string? title, object? errors)
    {
        Title = title ?? "Business Rule Violation";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2";
        Instance = instance;
        Extensions.Add("errors", errors);
        Extensions.Add("success", false);
    }
}
