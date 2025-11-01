using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.OperatorRoom;

public interface IFindOperatorRoomByIdQuery : ISingleQuerySpecification<Domain.Models.OperatorRoom.OperatorRoom>
{
    public int OperatorRoomId { get; init; }
}