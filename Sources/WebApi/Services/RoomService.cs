using WebApi.Dto.Requests;
using WebApi.Dto.Responses;
using WebApi.Dto.Room;
using WebApi.Models.Requests;

namespace WebApi.Services;

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