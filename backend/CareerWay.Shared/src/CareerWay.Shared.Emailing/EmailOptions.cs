namespace CareerWay.Shared.Emailing;

public class EmailOptions
{
    public string Host { get; set; } = default!;

    public int Port { get; set; }

    public bool EnableSsl { get; set; }

    public string SenderFullName { get; set; } = default!;

    public string SenderEmail { get; set; } = default!;

    public string Password { get; set; } = default!;
}
