using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Shared.AspNetCore.HttpProblemDetails;

public class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails(string? detail, string? instance)
    {
        Title = "Not Found";
        Detail = detail;
        Status = StatusCodes.Status404NotFound;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        Instance = instance;
        Extensions.Add("success", false);
    }
}
