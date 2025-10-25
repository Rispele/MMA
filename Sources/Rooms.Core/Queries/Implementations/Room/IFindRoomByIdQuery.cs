using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Room;

public interface IFindRoomByIdQuery : ISingleQuerySpecification<Models.Room.Room>
{
    public int RoomId { get; init; }
}