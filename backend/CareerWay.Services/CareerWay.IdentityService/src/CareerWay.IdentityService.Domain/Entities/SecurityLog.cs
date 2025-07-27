namespace CareerWay.IdentityService.Domain.Entities;

public class SecurityLog
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Action { get; set; } = default!;

    public DateTime CreationDate { get; set; }

    public User User { get; set; } = default!;
}
