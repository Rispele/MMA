namespace Rooms.Domain.Exceptions;

public class InstituteCoordinatorNotFoundException(string message) : DomainException(code: 404, errorCode: "InstituteResponsibleNotFound", message);