using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Shared.AspNetCore.HttpProblemDetails;

public class UnauthorizedAccessProblemDetails : ProblemDetails
{
    public UnauthorizedAccessProblemDetails(string? detail, string? instance)
    {
        Title = "Unauthorized Access";
        Detail = detail;
        Status = StatusCodes.Status401Unauthorized;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.2";
        Instance = instance;
        Extensions.Add("success", false);
    }
}
