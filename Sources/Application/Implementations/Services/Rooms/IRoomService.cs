using Application.Implementations.Dtos.Requests.RoomCreating;
using Application.Implementations.Dtos.Requests.RoomPatching;
using Application.Implementations.Dtos.Requests.RoomsQuerying;
using Application.Implementations.Dtos.Responses;
using Application.Implementations.Dtos.Room;

namespace Application.Implementations.Services.Rooms;

public interface IRoomService
{
    Task<RoomDto> GetRoomById(int id, CancellationToken cancellationToken);
    Task<RoomsResponseDto> GetRooms(RoomsRequestDto request, CancellationToken cancellationToken);
    Task<RoomDto> CreateRoom(PostRoomRequest request, CancellationToken cancellationToken);
    Task<RoomDto> PatchRoom(int roomId, PatchRoomRequestDto patchedDto, CancellationToken cancellationToken);
}
