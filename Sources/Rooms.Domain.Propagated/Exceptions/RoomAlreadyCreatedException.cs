using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class RoomAlreadyCreatedException(string message) : DomainException(code: 400, errorCode: "RoomAlreadyCreated", message);