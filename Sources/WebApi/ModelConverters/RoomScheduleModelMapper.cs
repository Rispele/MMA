using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Room.Requests;
using WebApi.Models.Requests.RoomSchedule;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class RoomScheduleModelMapper
{
    public static partial GetRoomScheduleDto MapGetRoomScheduleFromModel(GetRoomScheduleModel model);
}