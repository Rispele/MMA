using Microsoft.AspNetCore.JsonPatch;
using Rooms.Core.Spreadsheets;
using WebApi.ModelConverters;
using WebApi.Models.Files;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Responses;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;
using ICoreRoomService = Rooms.Core.Services.Interfaces.IRoomService;

namespace WebApi.Services.Implementations;

public class RoomService(ICoreRoomService roomService, SpreadsheetService spreadsheetService) : IRoomService
{
    public async Task<RoomsResponseModel> GetRoomsAsync(GetRoomsModel model, CancellationToken cancellationToken)
    {
        var getRoomsRequest = RoomModelsMapper.Map(model);

        var batch = await roomService.FilterRooms(getRoomsRequest, cancellationToken);

        return RoomModelsMapper.Map(batch);
    }

    public async Task<IEnumerable<AutocompleteRoomResponseModel>> AutocompleteRoomAsync(string roomName, CancellationToken cancellationToken)
    {
        var rooms = await roomService.AutocompleteRoom(roomName, cancellationToken);

        return rooms.Select(RoomModelsMapper.Map);
    }

    public async Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(id, cancellationToken);

        return RoomModelsMapper.Map(room);
    }

    public async Task<RoomModel> CreateRoom(CreateRoomModel model, CancellationToken cancellationToken)
    {
        var innerRequest = RoomModelsMapper.Map(model);

        var room = await roomService.CreateRoom(innerRequest, cancellationToken);

        return RoomModelsMapper.Map(room);
    }

    public async Task<(RoomModel? result, bool isOk)> PatchRoomAsync(
        int roomId,
        JsonPatchDocument<PatchRoomModel> patch,
        Func<PatchRoomModel, bool> validate,
        CancellationToken cancellationToken)
    {
        var patchModel = await GetPatchModel(roomId, cancellationToken);

        patch.ApplyTo(patchModel);

        return !validate(patchModel)
            ? (null, isOk: false)
            : (await PatchRoomAsync(roomId, patchModel, cancellationToken), isOk: true);
    }

    private async Task<PatchRoomModel> GetPatchModel(int roomId, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(roomId, cancellationToken);

        return RoomModelsMapper.MapToPatchRoomModel(room);
    }

    private async Task<RoomModel> PatchRoomAsync(
        int roomId,
        PatchRoomModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = RoomModelsMapper.Map(patchModel);

        var patched = await roomService.PatchRoom(roomId, patchRequest, cancellationToken);

        return RoomModelsMapper.Map(patched);
    }

    public async Task<FileExportModel> ExportRoomRegistry(Stream outputStream, CancellationToken cancellationToken)
    {
        var fileData = await spreadsheetService.ExportRoomRegistry(outputStream, cancellationToken);
        return new FileExportModel
        {
            FileName = fileData.FileName,
            ContentType = fileData.ContentType,
            Flush = fileData.Flush,
        };
    }
}