using Commons.Domain.Exceptions;

namespace Booking.Core.Exceptions;

public class InstituteCoordinatorNotFoundException(string message) : DomainException(code: 404, errorCode: "InstituteCoordinatorNotFound", message);