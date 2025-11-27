using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class EquipmentTypeNotFoundException(string message) : DomainException(code: 404, errorCode: "EquipmentTypeNotFound", message);