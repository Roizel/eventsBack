namespace EventTrackingSystem.Application.Common.Interfaces;

public interface ITelegramService
{
    Task SendMessageAsync(long chatId, string message);
}
