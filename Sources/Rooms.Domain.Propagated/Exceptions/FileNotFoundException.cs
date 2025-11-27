using Commons.Domain.Exceptions;

namespace Rooms.Domain.Propagated.Exceptions;

public class FileNotFoundException(string message) : DomainException(code: 404, errorCode: "FileNotFound", message);