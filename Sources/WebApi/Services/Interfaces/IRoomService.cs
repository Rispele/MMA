using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models.Files;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Responses;
using WebApi.Models.Room;

namespace WebApi.Services.Interfaces;

public interface IRoomService
{
    Task<RoomsResponseModel> GetRoomsAsync(GetRoomsModel model, CancellationToken cancellationToken);
    Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken);
    Task<RoomModel> CreateRoom(CreateRoomModel model, CancellationToken cancellationToken);

    Task<(RoomModel? result, bool isOk)> PatchRoomAsync(
        int roomId,
        JsonPatchDocument<PatchRoomModel> request,
        Func<PatchRoomModel, bool> validate,
        CancellationToken cancellationToken);
    Task<FileExportModel> ExportRoomRegistry(CancellationToken cancellationToken);
}