using Microsoft.AspNetCore.Identity;

namespace CareerWay.IdentityService.Domain.Entities;

public class User : IdentityUser<long>
{
    public DateTime LastLoginDate { get; set; }

    public DateTime CreationDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletionDate { get; set; }

    public ICollection<SecurityLog> SecurityLogs { get; set; } = [];

    public User()
    {
    }

    public User(string userName)
        : this()
    {
        UserName = userName;
    }
}
