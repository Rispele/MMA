using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.OperatorRoom;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.OperatorRoom;

public readonly struct FindOperatorRoomByIdQuery :
    IFindOperatorRoomByIdQuery,
    ISingleQueryImplementer<Domain.Models.OperatorRoom.OperatorRoom?, RoomsDbContext>
{
    public required int OperatorRoomId { get; init; }

    public Task<Domain.Models.OperatorRoom.OperatorRoom?> Apply(
        RoomsDbContext source,
        CancellationToken cancellationToken)
    {
        var id = OperatorRoomId;

        return source.OperatorRooms.FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}