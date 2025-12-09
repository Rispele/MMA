using Booking.Domain.Events.Payloads;
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Events;

[GenerateFieldNames]
public class BookingRequestEvent
{
    private readonly int? eventId = null!;

    [UsedImplicitly]
    private BookingRequestEvent()
    {
    }

    public BookingRequestEvent(int bookingRequestId, IBookingRequestEventPayload payload)
    {
        BookingRequestId = bookingRequestId;
        Payload = payload;
    }

    public int EventId => eventId ?? throw new InvalidOperationException("Id is not initialized yet");
    public int BookingRequestId { get; }
    public IBookingRequestEventPayload Payload { get; } = null!;
}