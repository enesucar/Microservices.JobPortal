namespace CareerWay.IdentityService.Application.Models;

public class CreateJobSeekerRequest
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}
