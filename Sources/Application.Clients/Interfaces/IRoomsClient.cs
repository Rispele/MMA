using Application.Clients.Dtos.Requests.RoomCreating;
using Application.Clients.Dtos.Requests.RoomPatching;
using Application.Clients.Dtos.Requests.RoomsQuerying;
using Application.Clients.Dtos.Responses;
using Application.Clients.Dtos.Room;

namespace Application.Clients.Interfaces;

public interface IRoomsClient
{
    Task<RoomDto> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken = default);
    Task<RoomsResponseDto> GetRoomsAsync(GetRoomsRequestDto request, CancellationToken cancellationToken = default);
    Task<RoomDto> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default);
    Task PatchRoomAsync(int roomId, PatchRoomRequest patch, CancellationToken cancellationToken = default);
}