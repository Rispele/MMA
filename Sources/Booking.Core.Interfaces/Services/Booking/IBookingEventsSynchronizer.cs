namespace Booking.Core.Interfaces.Services.Booking;

public interface IBookingEventsSynchronizer
{
    public Task<int> Synchronize(
        int fromEventId,
        int batchSize,
        CancellationToken cancellationToken);
}