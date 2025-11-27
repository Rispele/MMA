using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class RoomNotFoundException(string message) : DomainException(code: 404, errorCode: "RoomNotFound", message);