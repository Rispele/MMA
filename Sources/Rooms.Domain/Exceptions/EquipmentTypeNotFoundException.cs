namespace Rooms.Domain.Exceptions;

public class EquipmentTypeNotFoundException(string message) : DomainException(code: 404, errorCode: "EquipmentTypeNotFound", message);