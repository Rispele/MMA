namespace Rooms.Domain.Exceptions;

public class OperatorRoomNotFoundException(string message) : DomainException(code: 404, errorCode: "OperatorRoomNotFound", message);