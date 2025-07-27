using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace CareerWay.Shared.Emailing;

public class FakeEmailSender : IEmailSender
{
    private readonly ILogger<FakeEmailSender> _logger;

    public FakeEmailSender(ILogger<FakeEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(MailMessage mailMessage, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending email to {0} with subject {1}.", mailMessage.To.ToString(), mailMessage.Subject);
        return Task.CompletedTask;
    }
}
