namespace CareerWay.CompanyService.Application.Models;

public class CreateCompanyRequest
{
    public long Id { get; set; }

    public string Title { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime CreationDate { get; set; }
}
