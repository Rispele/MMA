using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Requests.RoomSchedule;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;

namespace Rooms.Core.Services.Implementations;

public class RoomScheduleService(IRoomScheduleClient roomScheduleClient,
    IRoomService roomService) : IRoomScheduleService
{
    public async Task<IEnumerable<RoomScheduleResponseDto>> GetRoomScheduleAsync(GetRoomScheduleDto dto, CancellationToken cancellationToken)
    {
        var existRoom = await roomService.GetRoomById(dto.RoomId, cancellationToken);
        if (existRoom == null)
        {
            throw new RoomNotFoundException("Room Not Found");
        }
        return await roomScheduleClient.GetRoomSchedule(dto);
    }
}