using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CareerWay.Shared.Storage.Azure;

public class AzureStorage : IAzureStorage
{
    public BlobServiceClient BlobServiceClient { get; }

    public StorageSharedKeyCredential StorageSharedKeyCredential { get; }

    public AzureStorage(IOptions<AzureBlobStorageOptions> options)
    {
        BlobServiceClient = new BlobServiceClient(options.Value.ConnectionString);
        StorageSharedKeyCredential = new StorageSharedKeyCredential(options.Value.AccountName, options.Value.AccountKey);
    }

    public List<string> GetFiles(string containerName)
    {
        var blobContainerClient = BlobServiceClient.GetBlobContainerClient(containerName);
        return blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public SasUriResult GenerateSasUri(
        string containerName,
        DateTimeOffset expiresOn,
        ResourceType resourceType,
        string? fileName = null,
        string? cacheControl = null,
        string? contentType = null,
        BlobSasPermissions permissions = BlobSasPermissions.All)
    {
        var fileExtension = Path.GetExtension(fileName);
        var blobName = fileExtension == null ? string.Empty : $"{Guid.NewGuid()}.{fileExtension}";
        var sasBuilder = new BlobSasBuilder(permissions, expiresOn)
        {
            BlobContainerName = containerName,
            BlobName = blobName,
            Resource = resourceType.GetDescription()
        };

        if (cacheControl != null) sasBuilder.CacheControl = cacheControl;
        if (contentType != null) sasBuilder.ContentType = contentType;

        var sasToken = sasBuilder.ToSasQueryParameters(StorageSharedKeyCredential).ToString();

        return new SasUriResult()
        {
            AccountName = StorageSharedKeyCredential.AccountName,
            ContainerName = containerName,
            FileName = fileName,
            Token = sasToken
        };
    }

    public async Task<string> UploadAsync(string containerName, IFormFile file)
    {
        var blobContainerClient = BlobServiceClient.GetBlobContainerClient(containerName);
        await blobContainerClient.CreateIfNotExistsAsync();
        await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{fileExtension}";

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        var header = new BlobHttpHeaders();
        header.ContentType = file.ContentType;

        return fileName;
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        var blobContainerClient = BlobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public bool HasFile(string containerName, string fileName)
    {
        var blobContainerClient = BlobServiceClient.GetBlobContainerClient(containerName);
        var result = blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
        return result;
    }
}
