using Commons;
using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Core.Dtos.Room;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.DtoMappers;

public static class OperatorDepartmentsDtoMapper
{
    public static OperatorDepartmentDto Map(OperatorDepartment entity)
    {
        return new OperatorDepartmentDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Rooms = entity.Rooms
                .Select(t => new OperatorDepartmentRoomInfoDto(
                    t.Id,
                    t.ScheduleAddress.Map(a => new ScheduleAddressDto(a.RoomNumber, a.Address))))
                .ToArray(),
            Operators = entity.Operators,
            Contacts = entity.Contacts
        };
    }
}