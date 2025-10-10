using Rooms.Core.Implementations.Dtos.Requests.RoomCreating;
using Rooms.Core.Implementations.Dtos.Requests.RoomPatching;
using Rooms.Core.Implementations.Dtos.Requests.RoomsQuerying;
using Rooms.Core.Implementations.Dtos.Responses;
using Rooms.Core.Implementations.Dtos.Room;

namespace Rooms.Core.Implementations.Services.Rooms;

public interface IRoomService
{
    Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken);
    Task<RoomsBatchDto> FilterRooms(GetRoomsRequest request, CancellationToken cancellationToken);
    Task<RoomDto> CreateRoom(CreateRoomRequest request, CancellationToken cancellationToken);
    Task<RoomDto> PatchRoom(int roomId, PatchRoomRequest request, CancellationToken cancellationToken);
}
