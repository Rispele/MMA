namespace Rooms.Domain.Exceptions;

public class FileNotFoundException(string message) : DomainException(404, "FileNotFound", message);