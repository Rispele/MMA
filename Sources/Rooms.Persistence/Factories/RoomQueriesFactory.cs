using Rooms.Domain.Queries.Factories;
using Rooms.Domain.Queries.Implementations.Room;
using Rooms.Persistence.Queries.Room;

namespace Rooms.Persistence.Factories;

public class RoomQueriesFactory : IRoomQueriesFactory
{
    public IFilterRoomsQuery Filter(int batchSize, int batchNumber, int afterRoomId = -1, RoomsFilter? filter = null)
    {
        return new FilterRoomsQuery
        {
            BatchSize = batchSize,
            BatchNumber = batchNumber,
            AfterRoomId = afterRoomId,
            Filter = filter
        };
    }

    public IFindRoomByIdQuery FindById(int roomId)
    {
        return new FindRoomByIdQuery
        {
            RoomId = roomId
        };
    }

    public IFindRoomByNameQuery FindByName(string name)
    {
        return new FindRoomByNameQuery
        {
            Name = name
        };
    }
}