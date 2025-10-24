using Rooms.Domain.Models.RoomModels;
using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Rooms;

public interface IFilterRoomsQuery<in TSource> : IQueryObject<Room, TSource>
    where TSource : IModelsSource
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterRoomId { get; init; }
    public RoomsFilter? Filter { get; init; }
}