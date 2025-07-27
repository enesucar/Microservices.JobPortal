namespace CareerWay.Shared.AspNetCore.Models;

public class ConflictErrorApiResponse : ErrorApiResponse
{
    public ConflictErrorApiResponse(string? detail = null, string? instance = null)
    {
        Title = "Conflict";
        Detail = detail;
        Status = StatusCodes.Status409Conflict;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
        Instance = instance;
    }
}
