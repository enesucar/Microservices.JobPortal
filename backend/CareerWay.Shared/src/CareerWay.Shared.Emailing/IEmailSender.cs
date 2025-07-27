using System.Net.Mail;

namespace CareerWay.Shared.Emailing;

public interface IEmailSender
{
    Task SendAsync(MailMessage mailMessage, CancellationToken cancellationToken = default);
}
