using Booking.Domain.Events;
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingProcesses;

[GenerateFieldNames]
public class BookingProcess
{
    private readonly int? id = null!;
    private readonly List<BookingEvent> bookingEvents = null!;
    private readonly List<DelayedEvent> delayedEvents = null!;

    [UsedImplicitly]
    private BookingProcess()
    {
    }

    public BookingProcess(int bookingRequestId)
    {
        bookingEvents = [];
        delayedEvents = [];

        BookingRequestId = bookingRequestId;
        State = BookingProcessState.Executing;
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public int BookingRequestId { get; }
    public BookingProcessState State { get; private set; }
    
    public IEnumerable<BookingEvent> BookingEvents => bookingEvents;
    public BookingEvent LastEvent => bookingEvents[^1];

    public void AddEvent(BookingEvent @event)
    {
        bookingEvents.Add(@event);
    }

    public void MarkDelayedEventAsRetried()
    {
        
    }

    public void MarkAsCrashed(int eventId)
    {
        var delayed = delayedEvents.FirstOrDefault(e => e.EventId == eventId);
        if (delayed is not null)
        {
            if (!delayed.MarkAttemptFailed())
            {
                SetRollingBack();                
            }
        }
        else
        {
            delayedEvents.Add(new DelayedEvent(eventId, Id));
        }
    }

    public void SetRollingBack()
    {
        State = BookingProcessState.RollingBack;
    }
}