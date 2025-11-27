
using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class BookingRequestNotFoundException(string message) : DomainException(code: 404, errorCode: "BookingRequestNotFound", message);