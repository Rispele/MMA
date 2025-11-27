using Commons.Domain.Exceptions;

namespace Rooms.Domain.Exceptions;

public class EquipmentNotFoundException(string message) : DomainException(code: 404, errorCode: "EquipmentNotFound", message);