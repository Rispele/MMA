using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Queries.Implementations.Room;

namespace Rooms.Core.Queries.Factories;

public interface IRoomQueriesFactory
{
    public IFilterRoomsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterRoomId = -1,
        RoomsFilterDto? filter = null);

    public IFindRoomByIdQuery FindById(int roomId);

    public IFindRoomByNameQuery FindByName(string name);
}