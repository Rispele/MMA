using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Rooms.Core.Clients.RoomSchedule;
using Rooms.Core.Dtos.Room.Requests;
using Rooms.Core.Services.Rooms.Interfaces;

namespace Rooms.Core.Services.Rooms;

public class ScheduleService(
    IRoomScheduleClient roomScheduleClient,
    IRoomService roomService,
    ILogger<ScheduleService> logger) : IScheduleService
{
    public async IAsyncEnumerable<RoomScheduleResponseItemDto> GetRoomSchedule(
        GetRoomScheduleDto dto,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var room = await roomService.GetRoomById(dto.RoomId, cancellationToken);

        if (room.ScheduleAddress is null)
        {
            throw new InvalidOperationException("Room address and number is not initialized");
        }

        var request = new GetRoomScheduleRequest(room.ScheduleAddress, dto.From, dto.To);
        var response = roomScheduleClient.GetRoomSchedule(request, cancellationToken);

        await foreach (var item in response)
        {
            if (item is null)
            {
                logger.LogError("Room Schedule returned null for room id {RoomId}", dto.RoomId);
                continue;
            }
            
            yield return item;
        }
    }
}