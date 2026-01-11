namespace Booking.Core.Interfaces.Services.Booking;

public interface IBookingService
{
    public Task InitiateBookingRequest(int bookingRequestId, CancellationToken cancellationToken);

    public Task SaveModerationResult(int bookingRequestId, bool isApproved, string moderatorComment, CancellationToken cancellationToken);

    public Task SaveEdmsResolutionResult(int bookingRequestId, bool isApproved, CancellationToken cancellationToken);
}