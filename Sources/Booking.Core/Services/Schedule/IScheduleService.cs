using Booking.Core.Dtos.Schedule;
using Commons.ExternalClients.RoomSchedule;

namespace Booking.Core.Services.Schedule;

public interface IScheduleService
{
    IAsyncEnumerable<RoomScheduleResponseItemDto> GetRoomSchedule(GetRoomScheduleDto dto, CancellationToken cancellationToken);
}