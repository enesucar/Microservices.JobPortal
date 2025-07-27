namespace CareerWay.Shared.AspNetCore.Models;

public class UnauthorizedAccesErrorApiResponse : ErrorApiResponse
{
    public UnauthorizedAccesErrorApiResponse(string? detail = null, string? instance = null)
    {
        Title = "Unauthorized Access";
        Detail = detail;
        Status = StatusCodes.Status401Unauthorized;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.2";
        Instance = instance;
    }
}
