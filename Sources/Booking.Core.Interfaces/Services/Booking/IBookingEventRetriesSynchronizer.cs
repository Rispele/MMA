namespace Booking.Core.Interfaces.Services.Booking;

public interface IBookingEventRetriesSynchronizer
{
    public Task RetryFailedProcesses(int batchSize, CancellationToken cancellationToken);
}