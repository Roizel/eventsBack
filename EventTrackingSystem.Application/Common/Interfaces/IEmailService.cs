using EventTrackingSystem.Application.Common.SMTP;

namespace EventTrackingSystem.Application.Common.Interfaces;


public interface IEmailService
{
    Task SendAsync(Message messageData);
}
