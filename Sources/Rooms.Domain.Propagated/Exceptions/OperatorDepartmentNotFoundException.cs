using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class OperatorDepartmentNotFoundException(string message) : DomainException(code: 404, errorCode: "OperatorDepartmentNotFound", message);