using Rooms.Core.Dtos.Room;
using Rooms.Core.Dtos.Room.Requests;
using Rooms.Core.Dtos.Room.Responses;

namespace Rooms.Core.Services.Rooms.Interfaces;

public interface IRoomService
{
    Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken);
    Task<RoomDto[]> FindRoomByIds(int[] roomIds, CancellationToken cancellationToken);
    Task<RoomsResponseDto> FilterRooms(GetRoomsRequestDto requestDto, CancellationToken cancellationToken);
    Task<IEnumerable<AutocompleteRoomResponseDto>> AutocompleteRoom(string roomName, CancellationToken cancellationToken);
    Task<RoomDto> CreateRoom(CreateRoomDto dto, CancellationToken cancellationToken);
    Task<RoomDto> PatchRoom(int roomId, PatchRoomDto dto, CancellationToken cancellationToken);
}