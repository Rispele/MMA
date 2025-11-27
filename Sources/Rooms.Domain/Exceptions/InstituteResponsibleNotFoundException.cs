using Commons.Domain.Exceptions;

namespace Rooms.Domain.Exceptions;

public class InstituteResponsibleNotFoundException(string message) : DomainException(code: 404, errorCode: "InstituteResponsibleNotFound", message);