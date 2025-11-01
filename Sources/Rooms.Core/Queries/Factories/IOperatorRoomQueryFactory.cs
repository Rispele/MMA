using Rooms.Core.Dtos.Requests.OperatorRooms;
using Rooms.Core.Queries.Implementations.OperatorRoom;

namespace Rooms.Core.Queries.Factories;

public interface IOperatorRoomQueryFactory
{
    public IFilterOperatorRoomsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterOperatorRoomId = -1,
        OperatorRoomsFilterDto? filter = null);

    public IFindOperatorRoomByIdQuery FindById(int operatorRoomId);
}