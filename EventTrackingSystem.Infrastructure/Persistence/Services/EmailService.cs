using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Application.Common.SMTP;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class EmailService(
    IOptions<EmailConfiguration> options
    ) : IEmailService
{
    private readonly EmailConfiguration emailConfiguration = options.Value;
    public async Task SendAsync(Message messageData)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Факультет Кібернетики МЕГУ", emailConfiguration.From));
        message.To.Add(new MailboxAddress(messageData.Name, messageData.To));
        message.Subject = "Анонс";

        string html = await File.ReadAllTextAsync("Templates/Invate.html");

        html = html.Replace("{Email}", messageData.To);
        html = html.Replace("{Name}", messageData.Name);
        html = html.Replace("{Title}", messageData.Title);
        html = html.Replace("{Preview}", messageData.Preview);
        html = html.Replace("{PreviewImg}", messageData.PreviewImg);
        html = html.Replace("{Location}", messageData.Location);
        html = html.Replace("{Date}", messageData.Date.ToString("dd.MM.yyyy HH:mm"));

        message.Body = new TextPart("html")
        {
            Text = html
        };


        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(emailConfiguration.SmtpServer, emailConfiguration.Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(emailConfiguration.UserName, emailConfiguration.Password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending: " + ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
