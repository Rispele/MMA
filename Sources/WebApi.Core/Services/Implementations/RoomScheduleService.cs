using Booking.Core.Interfaces.Services.Schedule;
using Commons;
using WebApi.Core.ModelConverters;
using WebApi.Core.Models.Requests.RoomSchedule;
using WebApi.Core.Models.Responses;
using WebApi.Core.Models.RoomSchedule;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Core.Services.Implementations;

public class RoomScheduleService(IScheduleService roomScheduleService) : IRoomScheduleService
{
    public async Task<RoomScheduleResponseModel> GetRoomSchedule(GetRoomScheduleModel model, CancellationToken cancellationToken)
    {
        var dto = RoomScheduleModelMapper.MapGetRoomScheduleFromModel(model);

        var scheduleResponseDtos = await roomScheduleService
            .GetRoomSchedule(dto, cancellationToken)
            .ToListAsync(cancellationToken);

        return new RoomScheduleResponseModel
        {
            ScheduleModels = scheduleResponseDtos.Select(item => new RoomScheduleModel
            {
                Date = item.Date,
                From = item.From,
                To = item.To,
                Teacher = item.Teacher,
                GroupTitle = item.GroupTitle,
                Title = item.Title
            }).ToArray(),
            Count = scheduleResponseDtos.Count
        };
    }
}