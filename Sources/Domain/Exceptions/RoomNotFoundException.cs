namespace Domain.Exceptions;

public class RoomNotFoundException(string message) : DomainException(404, "RoomNotFound", message);