using Microsoft.AspNetCore.Http;

namespace CareerWay.Shared.Storage;

public interface IStorage
{
    List<string> GetFiles(string path);

    Task<string> UploadAsync(string path, IFormFile file);

    Task DeleteAsync(string path, string fileName);

    bool HasFile(string containerName, string fileName);
}
