using WebApi.ModelConverters;
using WebApi.Models.Requests.RoomSchedule;
using WebApi.Models.Responses;
using WebApi.Models.RoomSchedule;
using WebApi.Services.Interfaces;
using ICoreRoomScheduleService = Rooms.Core.Services.Interfaces.IRoomScheduleService;

namespace WebApi.Services.Implementations;

public class RoomScheduleService(ICoreRoomScheduleService roomScheduleService) : IRoomScheduleService
{
    public async Task<RoomScheduleResponseModel> GetRoomSchedule(GetRoomScheduleModel model)
    {
        var dto = RoomScheduleModelMapper.MapGetRoomScheduleFromModel(model);

        var scheduleResponseDtos = await roomScheduleService.GetRoomScheduleAsync(dto);

        return new RoomScheduleResponseModel
        {
            ScheduleModels = scheduleResponseDtos.Select(x => new RoomScheduleModel
            {
                ClassTime = x.ClassTime,
                ClassEvent = x.ClassEvent,
                Teacher = x.Teacher,
                ClassGroup = x.ClassGroup,
            }).ToArray(),
            Count = scheduleResponseDtos.Count()
        };
    }
}