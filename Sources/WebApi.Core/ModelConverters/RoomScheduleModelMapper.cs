using Booking.Core.Interfaces.Dtos.Schedule;
using Riok.Mapperly.Abstractions;
using WebApi.Core.Models.Requests.RoomSchedule;

namespace WebApi.Core.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class RoomScheduleModelMapper
{
    public static partial GetRoomScheduleDto MapGetRoomScheduleFromModel(GetRoomScheduleModel model);
}