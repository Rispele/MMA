using Booking.Core.Interfaces.Dtos.Schedule;
using Riok.Mapperly.Abstractions;
using WebApi.Models.Requests.RoomSchedule;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class RoomScheduleModelMapper
{
    public static partial GetRoomScheduleDto MapGetRoomScheduleFromModel(GetRoomScheduleModel model);
}