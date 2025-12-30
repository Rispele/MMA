namespace Booking.Core.Interfaces.Services.Booking;

public interface IBookingProcessRollbackSynchronizer
{
    public Task RollbackProcesses(int batchSize, CancellationToken cancellationToken);
}