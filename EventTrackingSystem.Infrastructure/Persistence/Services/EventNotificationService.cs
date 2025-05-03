using EventTrackingSystem.Application.Common.Configurations;
using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Microsoft.Extensions.Configuration;

namespace EventTrackingSystem.Infrastructure.Persistence.Services;

public class EventNotificationService(
    AppDbContext context,
    IEmailService emailService,
    IOptions<EventNotificationSettings> notificationSettings,
    IConfiguration config
) : IEventNotificationService
{
    private readonly EventNotificationSettings settings = notificationSettings.Value;

    public async Task SendEventNotificationsAsync()
    {
        TelegramBotClient telegramBot = new TelegramBotClient(config.GetValue<String>("TelegramToken"));
        var notificationTime = DateTime.UtcNow.AddHours(settings.HoursBefore).AddMinutes(settings.MinutesBefore);

        var events = await context.Events
            .Where(e => e.Date >= DateTime.UtcNow && e.Date <= notificationTime)
            .Include(e => e.RoleEvents)
                .ThenInclude(re => re.Role)
                    .ThenInclude(r => r.UserRoles)
                        .ThenInclude(ur => ur.User)
            .ToListAsync();

        foreach (var eventEntity in events)
        {
            string telegramMessage = $"<b>Нагадування про подію:</b> {eventEntity.Title}\n\n" +
                          $"<i>Дата:</i> {eventEntity.Date.ToString("dd/MM/yyyy HH:mm")}\n" +
                          $"<u>Місце:</u> {eventEntity.Location}\n\n" +
                          $"<i>Опис:</i> {eventEntity.Preview ?? "Нагадуємо про подію!"}";


            foreach (var chatId in context.TelegramChats.Select(x => x.ChatId).ToList())
            {
                try
                {
                    // Перевірка на вже надіслане повідомлення
                    bool telegramAlreadySent = await context.EventNotificationLogs
                        .AnyAsync(n => n.EventId == eventEntity.Id && n.ChatId == chatId);

                    if (telegramAlreadySent)
                        continue;  // Якщо вже надіслано, пропускаємо

                    await telegramBot.SendTextMessageAsync(
                        chatId,
                        telegramMessage,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                    );

                    // Додаємо запис про надіслане повідомлення в лог
                    context.EventNotificationLogs.Add(new EventNotificationLog
                    {
                        EventId = eventEntity.Id,
                        ChatId = chatId,
                        SentAt = DateTime.UtcNow
                    });
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw new Exception("Error send telegram notification");
                }
            }

            foreach (var roleEvent in eventEntity.RoleEvents)
            {
                foreach (var userRole in roleEvent.Role.UserRoles)
                {
                    var user = userRole.User;

                    if (string.IsNullOrEmpty(user.Email))
                        continue;

                    bool alreadySent = await context.EventNotificationLogs
                        .AnyAsync(n => n.EventId == eventEntity.Id && n.UserId == user.Id);

                    if (alreadySent)
                        continue;

                    await emailService.SendAsync(new Application.Common.SMTP.Message
                    {
                        Name = $"{user.FirstName} {user.LastName}",
                        To = user.Email,
                        Title = $"Нагадування про подію: {eventEntity.Title}",
                        Preview = eventEntity.Preview ?? "Нагадуємо про подію!",
                        PreviewImg = eventEntity.PreviewPhoto ?? "",
                        //PreviewImg = "https://img.freepik.com/premium-vector/european-soccer-tournament-2020-2021-background-illustration_42237-738.jpg",
                        Location = eventEntity.Location,
                        Date = eventEntity.Date
                    });

                    context.EventNotificationLogs.Add(new EventNotificationLog
                    {
                        EventId = eventEntity.Id,
                        UserId = user.Id,
                        SentAt = DateTime.UtcNow
                    });
                    await context.SaveChangesAsync();

                }
            }
        }
    }

}
