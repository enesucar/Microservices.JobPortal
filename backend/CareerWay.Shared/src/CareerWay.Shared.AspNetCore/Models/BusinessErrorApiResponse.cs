namespace CareerWay.Shared.AspNetCore.Models;

public class BusinessErrorApiResponse : ErrorApiResponse
{
    public object? Errors { get; set; } = default!;

    public BusinessErrorApiResponse(string? detail = null, string? instance = null, string? title = null, object? errors = null)
    {
        Title = title ?? "Business Rule Violation";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2";
        Instance = instance;
        Errors = errors;
    }
}
