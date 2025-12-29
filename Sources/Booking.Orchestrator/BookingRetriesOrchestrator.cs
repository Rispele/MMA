using Booking.Core.Interfaces.Services.Booking;

namespace Booking.Orchestrator;

public class BookingRetriesOrchestrator(
    ILogger<BookingRetriesOrchestrator> logger,
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var scope = serviceProvider.CreateScope();
        var synchronizer = scope.ServiceProvider.GetRequiredService<IBookingEventRetriesSynchronizer>();

        while (!cancellationToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await synchronizer.RetryFailedProcesses(batchSize: 100, cancellationToken);

            await Task.Delay(10000, cancellationToken);
        }
    }
}