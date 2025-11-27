using Commons.Domain.Exceptions;

namespace Rooms.Domain.Exceptions;

public class EquipmentSchemaNotFoundException(string message) : DomainException(code: 404, errorCode: "EquipmentSchemaNotFound", message);