using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class InstituteResponsibleNotFoundException(string message) : DomainException(code: 404, errorCode: "InstituteResponsibleNotFound", message);