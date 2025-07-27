namespace CareerWay.UserRegistrationSaga.API.Models;

public class CreateCompanyRequest
{
    public string Title { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}
