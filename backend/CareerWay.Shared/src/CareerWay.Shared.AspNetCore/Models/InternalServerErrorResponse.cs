namespace CareerWay.Shared.AspNetCore.Models;

public class InternalServerErrorResponse : ErrorApiResponse
{
    public InternalServerErrorResponse(string? detail = null, string? instance = null)
    {
        Title = "Internal Server Error";
        Status = StatusCodes.Status500InternalServerError;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
        Detail = detail;
        Instance = instance;
    }
}
