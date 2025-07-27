using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Shared.AspNetCore.HttpProblemDetails;

public class ConflictProblemDetails : ProblemDetails
{
    public ConflictProblemDetails(string? detail, string? instance)
    {
        Title = "Conflict";
        Detail = detail;
        Status = StatusCodes.Status409Conflict;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
        Instance = instance;
        Extensions.Add("success", false);
    }
}
