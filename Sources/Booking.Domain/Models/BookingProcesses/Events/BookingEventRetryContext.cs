using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingProcesses.Events;

[GenerateFieldNames]
public class BookingEventRetryContext
{
    public const int AttemptsCount = 5;

    private readonly int? id = null!;

    [UsedImplicitly]
    private BookingEventRetryContext()
    {
    }

    public BookingEventRetryContext(int eventId, int processId)
    {
        EventId = eventId;
        Attempt = 0;
        ProcessId = processId;
        State = BookingEventRetryContextState.Retrying;
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public int EventId { get; }
    public int ProcessId { get; }

    public int Attempt { get; private set; }
    public DateTime RetryAt { get; private set; }
    public BookingEventRetryContextState State { get; private set; }

    public void MarkAttemptSucceeded()
    {
        ValidateContextInRetryingState();

        State = BookingEventRetryContextState.Succeeded;
        RetryAt = DateTime.MinValue;
    }

    public void MarkAttemptFailed()
    {
        ValidateContextInRetryingState();

        if (Attempt != AttemptsCount)
        {
            Attempt++;
            RetryAt = DateTime.UtcNow + TimeSpan.FromSeconds(Math.Pow(2, Attempt));
        }
        else
        {
            State = BookingEventRetryContextState.Failed;
        }
    }

    private void ValidateContextInRetryingState()
    {
        if (State is not BookingEventRetryContextState.Retrying)
        {
            throw new InvalidOperationException($"State is not {nameof(BookingEventRetryContextState.Retrying)}");
        }
    }
}