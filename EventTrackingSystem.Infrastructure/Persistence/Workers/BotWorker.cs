using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Infrastructure.Persistence.Workers;

public class BotWorker
{
    private readonly TelegramBotClient _telegramBot;
    private readonly IApplicationBuilder _app;

    public BotWorker(IApplicationBuilder app)
    {
        _app = app;

        using (var scope = _app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            _telegramBot = new TelegramBotClient(config.GetValue<String>("TelegramToken"));
            _telegramBot.StartReceiving(Update, Error);
        }
    }

    private async Task Update(ITelegramBotClient botCleint, Update update, CancellationToken token)
    {

        var message = update.Message;
        var callBackQuery = update.CallbackQuery;

        if (message == null || message.Type != MessageType.Text) return;

        //Тільки потрапив у чат
        if (message.Chat != null)
        {
            using (var scope = _app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                var telegramChat = context.TelegramChats
                    .Where(x => !x.IsDeleted)
                    .SingleOrDefault(x => x.ChatId == message.Chat.Id);

                if (telegramChat == null)
                {
                    telegramChat = mapper.Map<TelegramChatEntity>(message.Chat);
                    context.TelegramChats.Add(telegramChat);
                    context.SaveChanges();
                }
            }
        }
    }
    private Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        throw new NotImplementedException();
    }
}
