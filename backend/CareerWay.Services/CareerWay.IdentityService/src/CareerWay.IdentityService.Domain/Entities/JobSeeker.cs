namespace CareerWay.IdentityService.Domain.Entities;

public class JobSeeker : User
{
    public virtual string FirstName { get; set; } = default!;

    public virtual string LastName { get; set; } = default!;

    public virtual User User { get; set; } = default!;
}
