using Commons.Domain.Exceptions;

namespace Booking.Core.Exceptions;

public class InvalidRequestException(string message) : DomainException(400, "Booking.InvalidRequest", message);