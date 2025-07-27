namespace CareerWay.Shared.AspNetCore.Models;

public class BaseApiResponse
{
    public int Status { get; set; }

    public bool Success => Status < 400;
}
