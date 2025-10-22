namespace Rooms.Domain.Exceptions;

public class EquipmentNotFoundException(string message) : DomainException(404, "EquipmentNotFound", message);