using MapsterMapper;
using Microsoft.AspNetCore.JsonPatch;
using Rooms.Core.Dtos.Requests.Rooms;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Responses;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;
using ICoreRoomService = Rooms.Core.Services.Interfaces.IRoomService;

namespace WebApi.Services.Implementations;

public class RoomService(
    ICoreRoomService roomService,
    IMapper mapper) : IRoomService
{
    public async Task<RoomsResponseModel> GetRoomsAsync(GetRoomsModel model, CancellationToken cancellationToken)
    {
        var getRoomsRequest = mapper.Map<GetRoomsRequestDto>(model);

        var batch = await roomService.FilterRooms(getRoomsRequest, cancellationToken);

        return mapper.Map<RoomsResponseModel>(batch);
    }

    public async Task<RoomModel> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(id, cancellationToken);

        return mapper.Map<RoomModel>(room);
    }

    public async Task<RoomModel> CreateRoom(CreateRoomModel model, CancellationToken cancellationToken)
    {
        var innerRequest = mapper.Map<CreateRoomDto>(model);

        var room = await roomService.CreateRoom(innerRequest, cancellationToken);

        return mapper.Map<RoomModel>(room);
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
            : (mapper.Map<RoomModel>(await PatchRoomAsync(roomId, patchModel, cancellationToken)), isOk: true);
    }

    private async Task<PatchRoomModel> GetPatchModel(int roomId, CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(roomId, cancellationToken);

        return mapper.Map<PatchRoomModel>(room);
    }

    private async Task<RoomModel> PatchRoomAsync(
        int roomId,
        PatchRoomModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = mapper.Map<PatchRoomDto>(patchModel);

        var patched = await roomService.PatchRoom(roomId, patchRequest, cancellationToken);

        return mapper.Map<RoomModel>(patched);
    }
}