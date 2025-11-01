using Rooms.Core.Dtos.Requests.OperatorRooms;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.OperatorRoom;
using Rooms.Persistence.Queries.OperatorRoom;

namespace Rooms.Persistence.Factories;

public class OperatorRoomQueryFactory : IOperatorRoomQueryFactory
{
    public IFilterOperatorRoomsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterOperatorRoomId = -1,
        OperatorRoomsFilterDto? filter = null)
    {
        return new FilterOperatorRoomsQuery
        {
            BatchSize = batchSize,
            BatchNumber = batchNumber,
            AfterOperatorRoomId = afterOperatorRoomId,
            Filter = filter
        };
    }

    public IFindOperatorRoomByIdQuery FindById(int operatorRoomId)
    {
        return new FindOperatorRoomByIdQuery
        {
            OperatorRoomId = operatorRoomId
        };
    }
}