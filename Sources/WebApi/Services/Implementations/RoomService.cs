using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Responses;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;
using ICoreRoomService = Rooms.Core.Services.Interfaces.IRoomService;
using RoomsModelsConverter = WebApi.ModelConverters.RoomsModelsConverter;

namespace WebApi.Services.Implementations;

public class RoomService(ICoreRoomService roomService) : IRoomService
{
    public async Task<RoomsResponseModel> GetRoomsAsync(GetRoomsModel model, CancellationToken cancellationToken)
    {
        var getRoomsRequest = RoomsModelsConverter.Convert(model);

        var batch = await roomService.FilterRooms(getRoomsRequest, cancellationToken);

        return new RoomsResponseModel
        {
            Rooms = batch.Rooms.Select(RoomsModelsConverter.Convert).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(id, cancellationToken);

        return RoomsModelsConverter.Convert(room);
    }

    public async Task<RoomModel> CreateRoom(CreateRoomModel model, CancellationToken cancellationToken)
    {
        var innerRequest = RoomsModelsConverter.Convert(model);

        var room = await roomService.CreateRoom(innerRequest, cancellationToken);

        return RoomsModelsConverter.Convert(room);
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

        return RoomsModelsConverter.ConvertToPatchModel(room);
    }

    private async Task<RoomModel> PatchRoomAsync(int roomId, PatchRoomModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = RoomsModelsConverter.Convert(patchModel);

        var patched = await roomService.PatchRoom(roomId, patchRequest, cancellationToken);

        return RoomsModelsConverter.Convert(patched);
    }
}