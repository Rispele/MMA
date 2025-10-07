using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Models.Room;

namespace WebApi.Services.Interfaces;

public interface IRoomService
{
    Task<RoomsResponse> GetRoomsAsync(RoomsRequest request, CancellationToken cancellationToken);
    Task<RoomModel?> GetRoomByIdAsync(int id, CancellationToken cancellationToken);
    Task<RoomModel?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<RoomModel> CreateRoomAsync(PostRoomRequest request, CancellationToken cancellationToken);
    Task<PatchRoomModel?> GetPatchModelAsync(int roomId, CancellationToken cancellationToken);
    Task<RoomModel> ApplyPatchAndSaveAsync(int roomId, PatchRoomModel patchedModel, CancellationToken cancellationToken);
}