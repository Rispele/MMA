using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class InstituteCoordinatorNotFoundException(string message) : DomainException(code: 404, errorCode: "InstituteCoordinatorNotFound", message);