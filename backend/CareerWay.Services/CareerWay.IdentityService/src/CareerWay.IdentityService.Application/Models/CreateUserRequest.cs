namespace CareerWay.IdentityService.Application.Models;

public class CreateUserRequest
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string Role { get; set; } = default!;
}
