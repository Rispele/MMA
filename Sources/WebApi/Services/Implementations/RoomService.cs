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

    public Task<RoomModel> CreateRoomAsync(PostRoomRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PatchRoomModel?> GetPatchModelAsync(int roomId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomModel> ApplyPatchAndSaveAsync(int roomId, PatchRoomModel patchedModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}