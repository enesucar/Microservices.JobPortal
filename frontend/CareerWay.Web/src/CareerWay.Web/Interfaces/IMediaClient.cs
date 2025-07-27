namespace CareerWay.Web.Interfaces;

public interface IMediaClient
{
    Task<string> Create(IFormFile formFile);
}
