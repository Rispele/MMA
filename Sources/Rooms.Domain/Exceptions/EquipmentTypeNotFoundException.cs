namespace Rooms.Domain.Exceptions;

public class EquipmentTypeNotFoundException(string message) : DomainException(404, "EquipmentTypeNotFound", message);