using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Interfaces.Dtos.Room.Responses;
using Rooms.Core.Interfaces.Services.Rooms;

namespace Commons.Tests.Helpers.SDK.Rooms;

public class RoomsSdk(IRoomService roomService)
{
    public Task<RoomDto> CreateRoom(
        string name,
        Action<CreateRoomRequestBuilder>? builder = null,
        CancellationToken cancellationToken = default)
    {
        var request = CreateRoomRequestBuilder.Create(name);

        builder?.Invoke(request);

        return roomService.CreateRoom(request, cancellationToken);
    }

    public async Task<RoomDto> PatchRoom(int roomId, Action<PatchRoomRequestBuilder> builder, CancellationToken cancellationToken = default)
    {
        var room = await GetRoom(roomId, cancellationToken);

        var request = PatchRoomRequestBuilder.Create(room);

        builder(request);

        return await roomService.PatchRoom(roomId, request, cancellationToken);
    }

    public Task<RoomDto> GetRoom(int roomId, CancellationToken cancellationToken = default)
    {
        return roomService.GetRoomById(roomId, cancellationToken);
    }

    public Task<RoomsResponseDto> FilterRooms(
        int batchNumber = 0,
        int batchSize = 100,
        Action<FilterRoomsRequestBuilder>? builder = null,
        CancellationToken cancellationToken = default)
    {
        var request = FilterRoomsRequestBuilder.Create(batchNumber, batchSize);

        builder?.Invoke(request);

        return roomService.FilterRooms(request, cancellationToken);
    }
}