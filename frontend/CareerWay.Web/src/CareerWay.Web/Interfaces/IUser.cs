using CareerWay.Shared.Security.Users;

namespace CareerWay.Web.Interfaces;

public interface IUser : ICurrentUser
{
    public long Id { get; }
}
