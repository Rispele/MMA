using WebApi.Models.Requests.Rooms;
using WebApi.Models.Responses;
using WebApi.Models.Room;

namespace WebApi.Services.Interfaces;

public interface IRoomService
{
    Task<RoomsResponseModel> GetRoomsAsync(GetRoomsModel model, CancellationToken cancellationToken);
    Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken);
    Task<RoomModel> CreateRoomAsync(CreateRoomModel model, CancellationToken cancellationToken);
    Task<PatchRoomModel> GetPatchModel(int roomId, CancellationToken cancellationToken);
    Task<RoomModel> PatchRoomAsync(int roomId, PatchRoomModel request, CancellationToken cancellationToken);
}