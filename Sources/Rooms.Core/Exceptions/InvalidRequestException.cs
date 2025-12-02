using Commons.Domain.Exceptions;

namespace Rooms.Core.Exceptions;

public class InvalidRequestException(string message) : DomainException(400, "Room.InvalidRequest", message);