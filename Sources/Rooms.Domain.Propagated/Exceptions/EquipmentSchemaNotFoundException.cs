using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class EquipmentSchemaNotFoundException(string message) : DomainException(code: 404, errorCode: "EquipmentSchemaNotFound", message);