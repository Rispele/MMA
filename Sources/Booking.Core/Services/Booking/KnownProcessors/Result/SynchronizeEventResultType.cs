namespace Booking.Core.Services.Booking.KnownProcessors.Result;

public enum SynchronizeEventResultType
{
    Skipped,
    Success,
    Retry,
    Rollback
}