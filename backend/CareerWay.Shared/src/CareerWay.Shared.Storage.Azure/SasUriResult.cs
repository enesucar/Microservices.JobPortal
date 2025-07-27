namespace CareerWay.Shared.Storage.Azure;

public class SasUriResult
{
    public string Token { get; set; } = default!;

    public string? FileName { get; set; } = null;

    public string AccountName { get; set; } = default!;

    public string ContainerName { get; set; } = default!;
}
