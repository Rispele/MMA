using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingProcesses;

[GenerateFieldNames]
public class DelayedEvent
{
    public const int MaxAttempt = 10;

    private readonly int? id = null!;

    [UsedImplicitly]
    private DelayedEvent()
    {
    }

    public DelayedEvent(int eventId, int processId)
    {
        EventId = eventId;
        Attempt = 0;
        ProcessId = processId;
        State = DelayedEventState.Retrying;
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public int EventId { get; }
    public int ProcessId { get; }

    public int Attempt { get; private set; }
    public DateTime RetryAt { get; private set; }
    public DelayedEventState State { get; private set; }

    public bool MarkAttemptSucceeded()
    {
        
    }
    
    public bool MarkAttemptFailed()
    {
        Attempt++;
        
        if (ShouldRollback())
        {
            State = DelayedEventState.Failed;
            return false;
        }

        RetryAt = DateTime.UtcNow + TimeSpan.FromSeconds(Math.Pow(2, Attempt));
        return true;
    }
    
    private bool ShouldRollback()
    {
        return Attempt != MaxAttempt;
    }
}