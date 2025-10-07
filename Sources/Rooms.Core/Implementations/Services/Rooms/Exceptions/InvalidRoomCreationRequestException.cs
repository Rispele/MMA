using Rooms.Domain.Exceptions;

namespace Rooms.Core.Implementations.Services.Rooms.Exceptions;

public class InvalidRoomCreationRequestException(string message) : DomainException(404, $"RoomService.{nameof(InvalidRoomCreationRequestException)}", message)
{
}