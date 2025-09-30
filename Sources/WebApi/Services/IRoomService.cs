using WebApi.Dto.Requests;
using WebApi.Dto.Responses;
using WebApi.Dto.Room;
using WebApi.Models.Requests;

namespace WebApi.Services;

public interface IRoomService
{
    Task<RoomsResponse> GetRoomsAsync(RoomsRequest request, CancellationToken cancellationToken);
    Task<RoomModel?> GetRoomByIdAsync(int id, CancellationToken cancellationToken);
    Task<RoomModel?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<RoomModel> CreateRoomAsync(PostRoomRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Return model used for patching (PatchRoomModel filled from DB)
    /// </summary>
    Task<PatchRoomModel?> GetPatchModelAsync(int roomId, CancellationToken cancellationToken);

    Task<RoomModel> ApplyPatchAndSaveAsync(int roomId, PatchRoomModel patchedModel, CancellationToken cancellationToken);
}
