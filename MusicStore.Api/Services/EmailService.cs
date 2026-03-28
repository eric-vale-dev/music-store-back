namespace MusicStore.Api.Services;

using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task EnviarEmailAsync(string destinatario, string asunto, string mensajeHtml)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["Email:Username"]));
        email.To.Add(MailboxAddress.Parse(destinatario));
        email.Subject = asunto;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = mensajeHtml
        };
        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        // Conexión segura usando STARTTLS (Puerto 587)
        await smtp.ConnectAsync(_config["Email:Host"], int.Parse(_config["Email:Port"] ?? "587"), MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
