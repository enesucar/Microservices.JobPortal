using Asp.Versioning;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CareerWay.Shared.Guids; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CareerWay.MediaService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class JobSeekerProfilePhotosController : ControllerBase
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IGuidGenerator _guidGenerator;

    public JobSeekerProfilePhotosController(IOptions<AzureBlobStorageOptions> options, IGuidGenerator guidGenerator)
    {
        _blobServiceClient = new BlobServiceClient(options.Value.ConnectionString);
        _guidGenerator = guidGenerator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] IFormFile file)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient("jobseeker-profile-photos");
        await blobContainerClient.CreateIfNotExistsAsync();
        await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{_guidGenerator.Generate()}{fileExtension}";

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        var header = new BlobHttpHeaders();
        header.ContentType = file.ContentType;

        await blobClient.UploadAsync(file.OpenReadStream(), header); 

        return Created(string.Empty, fileName);
    }
}
