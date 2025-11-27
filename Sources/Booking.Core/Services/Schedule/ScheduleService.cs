using System.Runtime.CompilerServices;
using Booking.Core.Interfaces.Dtos.Schedule;
using Booking.Core.Interfaces.Services.Schedule;
using Commons.ExternalClients.RoomSchedule;
using Microsoft.Extensions.Logging;
using Rooms.Core.Interfaces.Services.Rooms;

namespace Booking.Core.Services.Schedule;

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

        var request = new GetRoomScheduleRequest(room.ScheduleAddress.RoomNumber, room.ScheduleAddress.Address, dto.From, dto.To);
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