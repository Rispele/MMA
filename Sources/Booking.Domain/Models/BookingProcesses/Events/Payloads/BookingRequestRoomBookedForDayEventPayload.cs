namespace Booking.Domain.Models.BookingProcesses.Events.Payloads;

public record BookingRequestRoomBookedForDayEventPayload(Guid BookingTimeKey, int Number, long EventId) : IBookingEventPayload;