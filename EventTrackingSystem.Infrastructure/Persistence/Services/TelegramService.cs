using EventTrackingSystem.Application.Common.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class TelegramService : ITelegramService
{
    private readonly ITelegramBotClient _botClient;

    public TelegramService(string botToken)
    {
        _botClient = new TelegramBotClient(botToken);
    }

    public async Task SendMessageAsync(long chatId, string message)
    {
        await _botClient.SendTextMessageAsync(new ChatId(chatId), message);
    }
}