using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments;
using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Interfaces.Dtos.Room.Fix;
using Rooms.Core.Interfaces.Dtos.Room.Parameters;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Domain.Models.Rooms;
using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Core.Services.Rooms.Mappers;

[Mapper]
internal static partial class RoomDtoMapper
{
    [MapProperty(nameof(Room.OperatorDepartment), nameof(RoomDto.OperatorDepartment),
        Use = nameof(MapOperatorDepartmentToDto))]
    public static partial RoomDto Map(Room room);
    public static partial RoomType Map(RoomTypeDto roomType);
    public static partial RoomLayout Map(RoomLayoutDto roomLayout);
    public static partial RoomNetType Map(RoomNetTypeDto roomNetType);
    public static partial RoomStatus Map(RoomStatusDto roomStatus);

    public static OperatorDepartmentDto? MapOperatorDepartmentToDto(OperatorDepartment? operatorDepartment)
    {
        return operatorDepartment == null ? null : new OperatorDepartmentDto
        {
            Id = operatorDepartment.Id,
            Name = operatorDepartment.Name,
            Rooms = operatorDepartment.Rooms.Select(x => new OperatorDepartmentRoomInfoDto
            {
                RoomId = x.Id,
                ScheduleAddress = x.ScheduleAddress == null ? null : new ScheduleAddressDto
                {
                    Address = x.ScheduleAddress.Address,
                    RoomNumber = x.ScheduleAddress.RoomNumber,
                    ScheduleRoomId = x.ScheduleAddress.ScheduleRoomId,
                }
            }).ToArray(),
            Operators = operatorDepartment.Operators,
            Contacts = operatorDepartment.Contacts
        };
    }
}