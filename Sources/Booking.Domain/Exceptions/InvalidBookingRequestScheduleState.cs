using Commons.Domain.Exceptions;

namespace Booking.Domain.Exceptions;

public class InvalidBookingRequestScheduleState(string message) : DomainException(400, $"Booking.{nameof(InvalidBookingRequestScheduleState)}", message);