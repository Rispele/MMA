using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Room;

public interface IFindRoomByNameQuery : ISingleQuerySpecification<Domain.Models.Room.Room>
{
    public string Name { get; init; }
}