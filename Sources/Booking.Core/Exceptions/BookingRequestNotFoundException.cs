using Commons.Domain.Exceptions;

namespace Booking.Core.Exceptions;

public class BookingRequestNotFoundException(string message) : DomainException(code: 404, errorCode: "BookingRequestNotFound", message);