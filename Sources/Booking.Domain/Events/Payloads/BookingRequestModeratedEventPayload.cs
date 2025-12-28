namespace Booking.Domain.Events.Payloads;

public class BookingRequestModeratedEventPayload(bool isApproved) : IBookingEventPayload;