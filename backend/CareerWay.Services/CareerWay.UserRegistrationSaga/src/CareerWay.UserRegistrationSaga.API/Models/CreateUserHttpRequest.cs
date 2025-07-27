namespace CareerWay.UserRegistrationSaga.API.Models;

public class CreateUserHttpRequest
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string Role { get; set; }
}
