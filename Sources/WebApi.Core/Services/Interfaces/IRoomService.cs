using Microsoft.AspNetCore.JsonPatch;
using WebApi.Core.Models.Files;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.Rooms;
using WebApi.Core.Models.Responses;
using WebApi.Core.Models.Room;

namespace WebApi.Core.Services.Interfaces;

public interface IRoomService
{
    Task<RoomsResponseModel> GetRoomsAsync(GetRequest<RoomsFilterModel> model, CancellationToken cancellationToken);
    Task<IEnumerable<AutocompleteRoomResponseModel>> AutocompleteRoomAsync(string roomName, CancellationToken cancellationToken);
    Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken);
    Task<RoomModel> CreateRoom(CreateRoomModel model, CancellationToken cancellationToken);

    Task<(RoomModel? result, bool isOk)> PatchRoomAsync(
        int roomId,
        JsonPatchDocument<PatchRoomModel> request,
        Func<PatchRoomModel, bool> validate,
        CancellationToken cancellationToken);
    Task<FileExportModel> ExportRoomRegistry(Stream outputStream, CancellationToken cancellationToken);
}