namespace Booking.Core.Interfaces.Services.Booking;

public interface IBookingService
{
    public Task SendBookingRequestForApprovalInEdms(int bookingRequestId, CancellationToken cancellationToken);
    
    public Task SaveEdmsApprovingResult(int bookingRequestId, CancellationToken cancellationToken);
}