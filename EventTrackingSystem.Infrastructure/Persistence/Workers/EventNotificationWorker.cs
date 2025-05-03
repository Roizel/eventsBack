using EventTrackingSystem.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventTrackingSystem.Infrastructure.Persistence.Workers;

public class EventNotificationWorker : BackgroundService
{
    private readonly ILogger<EventNotificationWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(60);

    public EventNotificationWorker(
        ILogger<EventNotificationWorker> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EventNotificationWorker запущено.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var eventNotificationService = scope.ServiceProvider.GetRequiredService<IEventNotificationService>();
                    await eventNotificationService.SendEventNotificationsAsync();
                }

                _logger.LogInformation("Надіслано нагадування про події.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка під час надсилання сповіщень про події.");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}