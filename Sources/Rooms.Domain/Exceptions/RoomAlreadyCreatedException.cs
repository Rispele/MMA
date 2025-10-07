namespace Rooms.Domain.Exceptions;

public class RoomAlreadyCreatedException(string message) : DomainException(400, "RoomAlreadyCreated", message);