namespace Booking.Domain.Models.BookingProcesses;

public enum DelayedEventState
{
    Retrying,
    Succeeded,
    Failed
}