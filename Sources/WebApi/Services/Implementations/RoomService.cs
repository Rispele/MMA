using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;
using ICoreRoomService = Rooms.Core.Implementations.Services.Rooms.IRoomService;

namespace WebApi.Services.Implementations;

public class RoomService(ICoreRoomService roomService, RoomsModelsConverter modelsConverter) : IRoomService
{
    public async Task<RoomsResponse> GetRoomsAsync(RoomsRequest request, CancellationToken cancellationToken)
    {
        var getRoomsRequest = modelsConverter.Convert(request);
        
        var batch = await roomService.FilterRooms(getRoomsRequest, cancellationToken);

        return new RoomsResponse
        {
            Rooms = batch.Rooms.Select(modelsConverter.Convert).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(id, cancellationToken);

        return modelsConverter.Convert(room);
    }

    public async Task<RoomModel> CreateRoomAsync(CreateRoomModel model, CancellationToken cancellationToken)
    {
        var innerRequest = modelsConverter.Convert(model);

        var room = await roomService.CreateRoom(innerRequest, cancellationToken);

        return modelsConverter.Convert(room);
    }

    public async Task<PatchRoomModel> GetPatchModel(int roomId, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(roomId, cancellationToken);

        return modelsConverter.ConvertToPatchModel(room);
    }

    public async Task<RoomModel> PatchRoomAsync(int roomId, PatchRoomModel patchModel, CancellationToken cancellationToken)
    {
        var patchRequest = modelsConverter.Convert(patchModel);
        
        var patched = await roomService.PatchRoom(roomId, patchRequest, cancellationToken);

        return modelsConverter.Convert(patched);
    }
}