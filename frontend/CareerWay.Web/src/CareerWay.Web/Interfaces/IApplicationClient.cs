using CareerWay.ApplicationService.API.Models;
using CareerWay.Web.Models.Applications;

namespace CareerWay.Web.Interfaces;

public interface IApplicationClient
{
    Task<ApplicantionsResponse> GetList(long postId);

    Task<bool> IsApplied(long postId);

    Task<int> GetCount(long postId);

    Task Apply(Application application);

    Task Withdraw(long postId);

    Task SetStatus(Application application);
}
