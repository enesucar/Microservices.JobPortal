using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CareerWay.Shared.Emailing.Mailkit;

public class MailkitEmailSender : IEmailSender
{
    private readonly EmailOptions _emailOptions;

    public MailkitEmailSender(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendAsync(System.Net.Mail.MailMessage mailMessage, CancellationToken cancellationToken = default)
    {
        MimeMessage message = new MimeMessage();
        message.To.AddRange(mailMessage.To.Select(o => new MailboxAddress(o.DisplayName, o.Address)));
        message.Bcc.AddRange(mailMessage.Bcc.Select(o => new MailboxAddress(o.DisplayName, o.Address)));
        message.Cc.AddRange(mailMessage.CC.Select(o => new MailboxAddress(o.DisplayName, o.Address)));
        message.From.Add(new MailboxAddress(_emailOptions.SenderFullName, _emailOptions.SenderEmail));
        message.Subject = mailMessage.Subject;

        BodyBuilder bodyBuilder = new BodyBuilder();

        if (mailMessage.Attachments.Count > 0)
        {
            foreach (var attachment in mailMessage.Attachments)
            {
                await bodyBuilder.Attachments.AddAsync(attachment.Name, attachment.ContentStream, cancellationToken);
            }
        }

        if (mailMessage.IsBodyHtml)
        {
            bodyBuilder.HtmlBody = mailMessage.Body;
        }
        else
        {
            bodyBuilder.TextBody = mailMessage.Body;
        }

        message.Body = bodyBuilder.ToMessageBody();

        using var emailClient = new SmtpClient();

        var connectAsync = emailClient.ConnectAsync(
            _emailOptions.Host,
            _emailOptions.Port,
            _emailOptions.EnableSsl,
            cancellationToken);

        var authenticateAsync = emailClient.AuthenticateAsync(
            _emailOptions.SenderEmail,
            _emailOptions.Password,
            cancellationToken);

        var sendAsync = emailClient.SendAsync(message);

        var disconnectAsync = emailClient.DisconnectAsync(true, cancellationToken);

        await Task.WhenAll(connectAsync, authenticateAsync, sendAsync, disconnectAsync);
    }
}
