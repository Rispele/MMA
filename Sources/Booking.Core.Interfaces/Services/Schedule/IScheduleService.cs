using Booking.Core.Interfaces.Dtos.Schedule;
using Commons.ExternalClients.RoomSchedule;

namespace Booking.Core.Interfaces.Services.Schedule;

public interface IScheduleService
{
    IAsyncEnumerable<RoomScheduleResponseItemDto> GetRoomSchedule(GetRoomScheduleDto dto, CancellationToken cancellationToken);
}