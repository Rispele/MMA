using Booking.Domain.Models.BookingProcesses.Events;
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingProcesses;

[GenerateFieldNames]
public class BookingProcess
{
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

    public void AddBookingEvent(BookingEvent @event)
    {
        bookingEvents.Add(@event);
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

        State = context.State switch
        {
            BookingEventRetryContextState.Failed => BookingProcessState.RollingBack,
            BookingEventRetryContextState.Retrying => BookingProcessState.Retrying,
            _ => State
        };
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