namespace Rooms.Domain.Exceptions;

public class EquipmentSchemaNotFoundException(string message) : DomainException(404, "EquipmentSchemaNotFound", message);