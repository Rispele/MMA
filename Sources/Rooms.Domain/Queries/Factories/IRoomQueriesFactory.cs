using Rooms.Domain.Queries.Implementations.Room;

namespace Rooms.Domain.Queries.Factories;

public interface IRoomQueriesFactory
{
    public IFilterRoomsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterRoomId = -1,
        RoomsFilter? filter = null);
    
    public IFindRoomByIdQuery FindById(int roomId);
    
    public IFindRoomByNameQuery FindByName(string name);
}