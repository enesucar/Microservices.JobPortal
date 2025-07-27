using Asp.Versioning;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using CareerWay.Shared.Guids; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Rest.Azure;
using System.ComponentModel;

namespace CareerWay.MediaService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class CompanyProfilePhotosController : ControllerBase
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IGuidGenerator _guidGenerator;

    public CompanyProfilePhotosController(IOptions<AzureBlobStorageOptions> options, IGuidGenerator guidGenerator)
    {
        _blobServiceClient = new BlobServiceClient(options.Value.ConnectionString);
        _guidGenerator = guidGenerator;
    }

    public enum ResourceType : byte
    {
        [Description("b")]
        Blob,

        [Description("c")]
        Container
    }


    [NonAction]
    public  string GetDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() as DescriptionAttribute;
        return attribute?.Description ?? value.ToString();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        ResourceType a = ResourceType.Container;
        return Ok(GetDescription(a));

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = "company-profile-photos",
            Resource = "c", // container
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(15)
        };

        // Upload için gerekli izinler: Write, Create, List (isteğe bağlı)
        sasBuilder.SetPermissions(BlobContainerSasPermissions.Write | BlobContainerSasPermissions.Create | BlobContainerSasPermissions.Read);
        var StorageSharedKeyCredential = new StorageSharedKeyCredential("careerway", "");
        // SAS token üret
        string sasToken = sasBuilder.ToSasQueryParameters(StorageSharedKeyCredential).ToString();

        return Ok(sasToken);
    }

    [HttpPost]
    public async Task<IActionResult> Create(IFormFile file)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient("company-profile-photos");
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
