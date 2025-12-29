namespace Booking.Domain.Models.BookingProcesses.Events.Payloads;

public class BookingRequestModeratedEventPayload(bool isApproved) : IBookingEventPayload;