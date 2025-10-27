using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public interface IFindRoomByIdQuery : ISingleQuerySpecification<Domain.Models.Room.Room>
{
    public int RoomId { get; init; }
}