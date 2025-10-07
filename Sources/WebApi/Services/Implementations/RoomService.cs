using Microsoft.AspNetCore.JsonPatch;
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

    public async Task<RoomModel> CreateRoomAsync(PostRoomRequest request, CancellationToken cancellationToken)
    {
        var innerRequest = modelsConverter.Convert(request);

        var room = await roomService.CreateRoom(innerRequest, cancellationToken);

        return modelsConverter.Convert(room);
    }

    public async Task<RoomModel> PatchRoomAsync(int roomId, JsonPatchDocument<PatchRoomModel> request, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(roomId, cancellationToken);
        
        var patchModel = modelsConverter.ConvertToPatchModel(room);
        
        request.ApplyTo(patchModel);
        
        var patchRequest = modelsConverter.Convert(patchModel);
        
        var patched = await roomService.PatchRoom(roomId, patchRequest, cancellationToken);

        return modelsConverter.Convert(patched);
    }
}