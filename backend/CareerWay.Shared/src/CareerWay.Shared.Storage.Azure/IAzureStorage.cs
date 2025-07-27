using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace CareerWay.Shared.Storage.Azure;

public interface IAzureStorage : IStorage
{
    public BlobServiceClient BlobServiceClient { get; }

    public StorageSharedKeyCredential StorageSharedKeyCredential { get; }

    SasUriResult GenerateSasUri(
        string containerName,
        DateTimeOffset expiresOn,
        ResourceType resourceType,
        string? fileName = null,
        string? cacheControl = null,
        string? contentType = null,
        BlobSasPermissions permissions = BlobSasPermissions.All);
}
