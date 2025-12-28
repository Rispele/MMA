namespace Booking.Domain.Events.Payloads;

public record BookingRequestResolvedInEdmsEventPayload(bool IsApproved) : IBookingEventPayload;