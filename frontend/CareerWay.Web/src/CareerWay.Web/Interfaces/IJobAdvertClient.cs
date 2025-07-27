using CareerWay.Web.Models.Post;

namespace CareerWay.Web.Interfaces;

public interface IJobAdvertClient
{
    Task<PostDetail> GetDetail(long id);

    Task<Departmants> GetDepartmants();

    Task<Positions> GetPositions();

    Task<Cities> GetCities();

    Task Create(CreatePostRequest request);

    Task Publish(PublishPostRequest request);
}
