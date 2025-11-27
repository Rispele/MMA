using Commons.Domain.Exceptions;

namespace Rooms.Domain.Exceptions;

public class RoomAlreadyCreatedException(string message) : DomainException(code: 400, errorCode: "RoomAlreadyCreated", message);