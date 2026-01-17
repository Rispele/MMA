using Microsoft.AspNetCore.JsonPatch;
using Rooms.Core.Interfaces.Services.Spreadsheets;
using WebApi.Core.ModelConverters;
using WebApi.Core.Models.Files;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.Rooms;
using WebApi.Core.Models.Responses;
using WebApi.Core.Models.Room;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Core.Services.Implementations;

public class RoomService(Rooms.Core.Interfaces.Services.Rooms.IRoomService roomService, ISpreadsheetService spreadsheetService) : IRoomService
{
    public async Task<RoomsResponseModel> GetRoomsAsync(GetRequest<RoomsFilterModel> model, CancellationToken cancellationToken)
    {
        var getRoomsRequest = RoomModelMapper.Map(model);

        var batch = await roomService.FilterRooms(getRoomsRequest, cancellationToken);

        return RoomModelMapper.Map(batch);
    }

    public async Task<IEnumerable<AutocompleteRoomResponseModel>> AutocompleteRoomAsync(string roomName, CancellationToken cancellationToken)
    {
        var rooms = await roomService.AutocompleteRoom(roomName, cancellationToken);

        return rooms.Select(RoomModelMapper.Map);
    }

    public async Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(id, cancellationToken);

        return RoomModelMapper.Map(room);
    }

    public async Task<RoomModel> CreateRoom(CreateRoomModel model, CancellationToken cancellationToken)
    {
        var innerRequest = RoomModelMapper.Map(model);

        var room = await roomService.CreateRoom(innerRequest, cancellationToken);

        return RoomModelMapper.Map(room);
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

        return RoomModelMapper.MapToPatchRoomModel(room);
    }

    private async Task<RoomModel> PatchRoomAsync(
        int roomId,
        PatchRoomModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = RoomModelMapper.Map(patchModel);

        var patched = await roomService.PatchRoom(roomId, patchRequest, cancellationToken);

        return RoomModelMapper.Map(patched);
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