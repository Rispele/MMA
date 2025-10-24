using Rooms.Domain.Models.RoomModels;
using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Rooms;

public interface IFindRoomByIdQuery<in TSource> : ISingleQueryObject<Room?, TSource>
    where TSource : IModelsSource
{
    public int RoomId { get; init; }
}