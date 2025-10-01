using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class RoomService : IRoomService
{
    public Task<RoomsResponse> GetRoomsAsync(RoomsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomModel?> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomModel?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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