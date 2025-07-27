using CareerWay.Shared.AspNetCore.Models;

namespace CareerWay.UserRegistrationSaga.API.Models;

public class ErrorApiJsonResponse : ErrorApiResponse
{
    public string JsonData { get; set; }

    public ErrorApiJsonResponse(string jsonData)
    {
        JsonData = jsonData; 
    }
}
