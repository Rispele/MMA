using Commons.Domain.Exceptions;

namespace Rooms.Domain.Exceptions;

public class FileNotFoundException(string message) : DomainException(code: 404, errorCode: "FileNotFound", message);