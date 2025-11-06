namespace Rooms.Domain.Exceptions;

public class OperatorDepartmentNotFoundException(string message) : DomainException(code: 404, errorCode: "OperatorDepartmentNotFound", message);