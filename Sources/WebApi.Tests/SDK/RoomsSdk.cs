using Rooms.Core.Implementations.Dtos.Responses;
using Rooms.Core.Implementations.Dtos.Room;
using Rooms.Core.Implementations.Services.Rooms;

namespace WebApi.Tests.SDK;

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

    public Task<RoomsBatchDto> GetRooms(
        int batchNumber = 0,
        int batchSize = 100,
        Action<GetRoomsRequestBuilder>? builder = null,
        CancellationToken cancellationToken = default)
    {
        var request = GetRoomsRequestBuilder.Create(batchNumber, batchSize);

        builder?.Invoke(request);

        return roomService.FilterRooms(request, cancellationToken);
    }
}