namespace Booking.Domain.Models.BookingProcesses;

public enum BookingProcessState
{
    Executing,
    Finished,
    Retrying,
    RollingBack,
    RolledBack
}