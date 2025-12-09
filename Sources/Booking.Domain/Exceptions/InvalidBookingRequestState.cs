using Commons.Domain.Exceptions;

namespace Booking.Domain.Exceptions;

public class InvalidBookingRequestState(string message) : DomainException(400, $"Booking.{nameof(InvalidBookingRequestState)}", message);