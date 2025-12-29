namespace Booking.Domain.Models.BookingProcesses.Events.Payloads;

public record BookingRequestResolvedInEdmsEventPayload(bool IsApproved) : IBookingEventPayload;