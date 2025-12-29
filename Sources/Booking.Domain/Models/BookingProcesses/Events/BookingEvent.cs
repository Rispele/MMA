using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingProcesses.Events;

[GenerateFieldNames]
public class BookingEvent
{
    private readonly int? id = null!;

    [UsedImplicitly]
    private BookingEvent()
    {
    }

    public BookingEvent(int bookingRequestId, IBookingEventPayload payload)
    {
        BookingRequestId = bookingRequestId;
        Payload = payload;
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public int BookingRequestId { get; }
    public IBookingEventPayload Payload { get; } = null!;
    public bool RolledBack { get; private set; }

    internal void Rollback()
    {
        RolledBack = true;
    }
}