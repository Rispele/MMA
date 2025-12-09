using Booking.Core.Interfaces.Services.Booking;

namespace Booking.Orchestrator;

public class BookingOrchestrator(
    ILogger<BookingOrchestrator> logger,
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var scope = serviceProvider.CreateScope();
        var synchronizer = scope.ServiceProvider.GetRequiredService<IBookingEventsSynchronizer>();

        var offset = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            offset = await synchronizer.Synchronize(offset, 100, cancellationToken);

            await Task.Delay(1000, cancellationToken);
        }
    }
}