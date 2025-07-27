namespace CareerWay.Shared.AspNetCore.Models;

public class ErrorApiResponse : BaseApiResponse
{
    public string? Type { get; set; }

    public string? Title { get; set; }

    public string? Detail { get; set; }

    public string? Instance { get; set; }
}
