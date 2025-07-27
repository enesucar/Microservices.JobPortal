using CareerWay.Shared.Validation.Validation;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Shared.AspNetCore.HttpProblemDetails;

public class ValidationFailureProblemDetails : ProblemDetails
{
    public ValidationFailureProblemDetails(string? detail, string? instance, List<ValidationFailureResult> errors)
    {
        Title = "Validation Failure";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        Instance = instance;
        Extensions.Add("errors", errors);
        Extensions.Add("success", false);
    }
}
