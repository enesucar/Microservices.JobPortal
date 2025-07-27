﻿namespace CareerWay.MediaService.API;

public class AzureBlobStorageOptions
{
    public string ConnectionString { get; set; } = default!;

    public string? AccountName { get; set; } = null;

    public string? AccountKey { get; set; } = null;
}
