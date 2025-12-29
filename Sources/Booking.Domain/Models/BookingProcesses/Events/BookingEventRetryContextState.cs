namespace Booking.Domain.Models.BookingProcesses.Events;

public enum BookingEventRetryContextState
{
    Retrying,
    Succeeded,
    Failed
}