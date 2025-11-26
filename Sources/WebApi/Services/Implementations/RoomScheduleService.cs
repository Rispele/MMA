using WebApi.ModelConverters;
using WebApi.Models.Requests.RoomSchedule;
using WebApi.Models.Responses;
using WebApi.Models.RoomSchedule;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class RoomScheduleService(Rooms.Core.Services.Rooms.Interfaces.IRoomScheduleService roomScheduleService) : IRoomScheduleService
{
    public async Task<RoomScheduleResponseModel> GetRoomSchedule(GetRoomScheduleModel model, CancellationToken cancellationToken)
    {
        var dto = RoomScheduleModelMapper.MapGetRoomScheduleFromModel(model);

        var scheduleResponseDtos = await roomScheduleService.GetRoomScheduleAsync(dto, cancellationToken);

        return new RoomScheduleResponseModel
        {
            ScheduleModels = scheduleResponseDtos.Select(x => new RoomScheduleModel
            {
                ClassTime = x.ClassTime,
                ClassEvent = x.ClassEvent,
                Teacher = x.Teacher,
                ClassGroup = x.ClassGroup
            }).ToArray(),
            Count = scheduleResponseDtos.Count()
        };
    }
}