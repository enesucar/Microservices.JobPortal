namespace CareerWay.Shared.AspNetCore.Models;

public class NotFoundErrorApiResponse : ErrorApiResponse
{
    public NotFoundErrorApiResponse(string? detail = null, string? instance = null)
    {
        Title = "Not Found";
        Detail = detail;
        Status = StatusCodes.Status404NotFound;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        Instance = instance;
    }
}
