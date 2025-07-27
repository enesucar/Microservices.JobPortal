using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Shared.AspNetCore.HttpProblemDetails;

public class InternalServerErrorProblemDetails : ProblemDetails
{
    public InternalServerErrorProblemDetails(string? detail, string? instance)
    {
        Title = "Internal Server Error";
        Status = StatusCodes.Status500InternalServerError;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
        Detail = detail;
        Instance = instance;
        Extensions.Add("success", false);
    }
}
