namespace Booking.Domain.Models.BookingProcesses;

public enum BookingProcessState
{
    Executing,
    Retrying,
    RollingBack,
    RolledBack
}