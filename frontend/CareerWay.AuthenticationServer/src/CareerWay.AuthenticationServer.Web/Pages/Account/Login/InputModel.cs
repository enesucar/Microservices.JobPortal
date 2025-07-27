using System.ComponentModel.DataAnnotations;

namespace CareerWay.AuthenticationServer.Web.Pages.Account.Login;

public class InputModel
{
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    public bool RememberLogin { get; set; }

    public string ReturnUrl { get; set; } = default!;

    public string ClientId { get; set; }
}
