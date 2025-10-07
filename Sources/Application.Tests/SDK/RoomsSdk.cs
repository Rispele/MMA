using Application.Clients.Dtos.Responses;
using Application.Clients.Dtos.Room;
using Application.Clients.Interfaces;

namespace Application.Tests.SDK;

public class RoomsSdk(IRoomsClient roomsClient)
{
    public Task<RoomDto> CreateRoom(
        string name,
        Action<CreateRoomRequestBuilder>? builder = null,
        CancellationToken cancellationToken = default)
    {
        var request = CreateRoomRequestBuilder.Create(name);

        builder?.Invoke(request);

        return roomsClient.CreateRoomAsync(request, cancellationToken);
    }

    public Task PatchRoom(int roomId, Action<>)
    {
        return roomsClient.PatchRoomAsync()
    }

    public Task<RoomDto> GetRoom(int roomId, CancellationToken cancellationToken = default)
    {
        return roomsClient.GetRoomByIdAsync(roomId, cancellationToken);
    }

    public Task<RoomsResponseDto> GetRooms(
        int batchNumber = 0,
        int batchSize = 100,
        Action<GetRoomsRequestBuilder>? builder = null,
        CancellationToken cancellationToken = default)
    {
        var request = GetRoomsRequestBuilder.Create(batchNumber, batchSize);

        builder?.Invoke(request);

        return roomsClient.GetRoomsAsync(request, cancellationToken);
    }
}