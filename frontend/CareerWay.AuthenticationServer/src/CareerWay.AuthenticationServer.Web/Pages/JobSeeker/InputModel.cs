using System.ComponentModel.DataAnnotations;

namespace CareerWay.AuthenticationServer.Web.Pages.JobSeeker;

public class InputModel
{
    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    public string ReturnUrl { get; set; } = default!;
}
