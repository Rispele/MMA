using Booking.Domain.Models.BookingProcesses.Events;
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingProcesses;

[GenerateFieldNames]
public class BookingProcess
{
    private const int RollbackTimeout = 2;

    private readonly int? id = null!;
    private readonly List<BookingEvent> bookingEvents = null!;
    private readonly List<BookingEventRetryContext> retryContexts = null!;

    [UsedImplicitly]
    private BookingProcess()
    {
    }

    public BookingProcess(int bookingRequestId)
    {
        bookingEvents = [];
        retryContexts = [];

        BookingRequestId = bookingRequestId;
        State = BookingProcessState.Executing;
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public int BookingRequestId { get; }
    public BookingProcessState State { get; private set; }

    public int RollbackAttempt { get; private set; }
    public DateTime? RollbackAt { get; private set; }
    
    public IEnumerable<BookingEvent> BookingEvents => bookingEvents;
    public IEnumerable<BookingEventRetryContext> BookingRetryContexts => retryContexts;

    public void AddBookingEvent(BookingEvent @event)
    {
        bookingEvents.Add(@event);
    }

    public IEnumerable<BookingEvent> GetEventsOfType<TEventPayload>()
    {
        return bookingEvents.Where(e => e.Payload.GetType() == typeof(TEventPayload));
    }

    #region Event Processing

    public void SetProcessAsFinished()
    {
        State = BookingProcessState.Finished;
    }

    public IEnumerable<BookingEvent> GetEventsToRetry()
    {
        return retryContexts
            .Where(t => t.State is BookingEventRetryContextState.Retrying)
            .SelectMany(t => bookingEvents.Where(e => e.Id == t.EventId));
    }

    public void MarkEventProcessAttemptSucceeded(int eventId)
    {
        ValidateState(BookingProcessState.Retrying, "Process is not retrying now");

        var context = FindRetryContext(eventId);
        context?.MarkAttemptSucceeded();

        State = BookingProcessState.Executing;
    }

    public void MarkEventProcessAttemptFailed(int eventId)
    {
        var context = GetOrCreateRetryContext(eventId);
        context.MarkAttemptFailed();

        switch (context.State)
        {
            case BookingEventRetryContextState.Failed:
                State = BookingProcessState.RollingBack;

                RollbackAttempt = 0;
                UpdateRollbackAtTime();
                break;
            case BookingEventRetryContextState.Retrying:
                State = BookingProcessState.Retrying;
                break;
            case BookingEventRetryContextState.Succeeded:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion

    #region Rolling Back

    public void RollBackEvent(int eventId)
    {
        ValidateState(BookingProcessState.RollingBack, "Could not rollback event when not in rolling back state");

        var @event = bookingEvents.FirstOrDefault(e => e.Id == eventId);
        @event?.Rollback();

        if (bookingEvents.All(t => t.RolledBack))
        {
            State = BookingProcessState.RolledBack;
        }
    }

    public void SetRollbackAttemptAsFailed(int eventId)
    {
        RollbackAttempt++;
        UpdateRollbackAtTime();
    }

    #endregion

    private void UpdateRollbackAtTime()
    {
        RollbackAt = DateTime.UtcNow + TimeSpan.FromSeconds(Math.Pow(RollbackTimeout, RollbackAttempt));
    }

    private BookingEventRetryContext GetOrCreateRetryContext(int eventId)
    {
        var context = FindRetryContext(eventId);

        if (context is not null)
        {
            return context;
        }

        context = new BookingEventRetryContext(eventId, Id);
        retryContexts.Add(context);

        return context;
    }

    private BookingEventRetryContext? FindRetryContext(int eventId)
    {
        return retryContexts.FirstOrDefault(e => e.EventId == eventId);
    }

    public void ValidateState(BookingProcessState expected, string errorMessage)
    {
        if (State != expected)
        {
            throw new InvalidOperationException(errorMessage);
        }
    }
}