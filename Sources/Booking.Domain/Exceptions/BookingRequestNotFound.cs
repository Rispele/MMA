using Commons.Domain.Exceptions;

namespace Booking.Domain.Exceptions;

public class BookingRequestNotFound(string message) : DomainException(400, $"Booking.{nameof(BookingRequestNotFound)}", message);