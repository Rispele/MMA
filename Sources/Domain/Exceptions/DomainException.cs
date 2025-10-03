namespace Domain.Exceptions;

public class DomainException(int code, string errorCode, string message) : Exception;