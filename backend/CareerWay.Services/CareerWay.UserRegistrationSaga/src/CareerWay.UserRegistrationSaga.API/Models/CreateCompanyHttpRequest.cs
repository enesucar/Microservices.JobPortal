namespace CareerWay.UserRegistrationSaga.API.Models;

public class CreateCompanyHttpRequest
{
    public long Id { get; set; }

    public string Title { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime CreationDate { get; set; }
}
