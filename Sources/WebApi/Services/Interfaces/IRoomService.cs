using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Models.Room;

namespace WebApi.Services.Interfaces;

public interface IRoomService
{
    Task<RoomsResponse> GetRoomsAsync(RoomsRequest request, CancellationToken cancellationToken);
    Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken);
    Task<RoomModel> CreateRoomAsync(PostRoomRequest request, CancellationToken cancellationToken);
    Task<RoomModel> PatchRoomAsync(int roomId, JsonPatchDocument<PatchRoomModel> request, CancellationToken cancellationToken);
}