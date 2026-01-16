using Commons.Domain.Exceptions;

namespace Booking.Core.Exceptions;

public class InstituteNotFoundException(string message) : DomainException(code: 404, errorCode: "InstituteNotFound", message);