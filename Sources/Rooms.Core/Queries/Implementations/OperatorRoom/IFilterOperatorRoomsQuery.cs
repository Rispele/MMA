using Rooms.Core.Dtos.Requests.OperatorRooms;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.OperatorRoom;

public interface IFilterOperatorRoomsQuery : IQuerySpecification<Domain.Models.OperatorRoom.OperatorRoom>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterOperatorRoomId { get; init; }
    public OperatorRoomsFilterDto? Filter { get; init; }
}