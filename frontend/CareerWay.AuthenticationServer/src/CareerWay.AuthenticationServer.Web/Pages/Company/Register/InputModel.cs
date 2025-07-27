using System.ComponentModel.DataAnnotations;

namespace CareerWay.AuthenticationServer.Web.Pages.Company.Register;

public class InputModel
{
    [Required]
    public string Password { get; set; } = default!;

    public string ReturnUrl { get; set; } = default!;

    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public string Email { get; set; } = default!;
}
