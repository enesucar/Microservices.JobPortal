using CareerWay.Shared.Validation.Validation;
using CareerWay.Shared.AspNetCore.HttpProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.AspNetCore.Http;

public static class HttpResponseExtensions
{
    public static string? GetHeaders(this HttpResponse httpResponse)
    {
        if (httpResponse.Headers.Keys.Count == 0)
        {
            return null;
        }

        string? header = httpResponse.Headers.Select(x => x.ToString()).Aggregate((key, value) => key + ", " + value);
        return string.IsNullOrEmpty(header) ? null : header;
    }

    public async static Task WriteValidationFailureProblemDetailsAsJsonAsync(this HttpResponse httpResponse, string detail, List<ValidationFailureResult> errors)
    {
        var problemDetails = new ValidationFailureProblemDetails(detail, httpResponse.HttpContext.Request.Path, errors);
        await WriteProblemDetailsAsJsonAsync(httpResponse, problemDetails);
    }

    public async static Task WriteUnauthorizedAccessProblemDetailsAsJsonAsync(this HttpResponse httpResponse, string? detail)
    {
        var problemDetails = new UnauthorizedAccessProblemDetails(detail, httpResponse.HttpContext.Request.Path);
        await WriteProblemDetailsAsJsonAsync(httpResponse, problemDetails);
    }

    public async static Task WriteForbiddenAccessProblemDetailsAsJsonAsync(this HttpResponse httpResponse, string? detail)
    {
        var problemDetails = new ForbiddenAccessProblemDetails(detail, httpResponse.HttpContext.Request.Path);
        await WriteProblemDetailsAsJsonAsync(httpResponse, problemDetails);
    }

    public async static Task WriteNotFoundProblemDetailsAsJsonAsync(this HttpResponse httpResponse, string? detail)
    {
        var problemDetails = new NotFoundProblemDetails(detail, httpResponse.HttpContext.Request.Path);
        await WriteProblemDetailsAsJsonAsync(httpResponse, problemDetails);
    }

    public async static Task WriteConflictProblemDetailsAsJsonAsync(this HttpResponse httpResponse, string? detail)
    {
        var problemDetails = new ConflictProblemDetails(detail, httpResponse.HttpContext.Request.Path);
        await WriteProblemDetailsAsJsonAsync(httpResponse, problemDetails);
    }

    public async static Task WriteBusinessProblemDetailsAsJsonAsync(this HttpResponse httpResponse, string? detail, string? title, object? errors)
    {
        var problemDetails = new BusinessProblemDetails(detail, httpResponse.HttpContext.Request.Path, title, errors);
        await WriteProblemDetailsAsJsonAsync(httpResponse, problemDetails);
    }

    public async static Task WriteInternalServerErrorProblemDetailsAsJsonAsync(this HttpResponse httpResponse, string? detail)
    {
        var problemDetails = new InternalServerErrorProblemDetails(detail, httpResponse.HttpContext.Request.Path);
        await WriteProblemDetailsAsJsonAsync(httpResponse, problemDetails);
    }

    private async static Task WriteProblemDetailsAsJsonAsync(HttpResponse httpResponse, ProblemDetails problemDetails)
    {
        await httpResponse.WriteAsJsonAsync(problemDetails);
    }
}
