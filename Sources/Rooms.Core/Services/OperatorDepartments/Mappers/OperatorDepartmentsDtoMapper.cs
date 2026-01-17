using Commons;
using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments;
using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Core.Services.OperatorDepartments.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class OperatorDepartmentsDtoMapper
{
    [MapProperty(nameof(OperatorDepartment.Id), nameof(OperatorDepartmentDto.Id))]
    [MapProperty(nameof(OperatorDepartment.Name), nameof(OperatorDepartmentDto.Name))]
    [MapProperty(nameof(OperatorDepartment.Operators), nameof(OperatorDepartmentDto.Operators))]
    [MapProperty(nameof(OperatorDepartment.Contacts), nameof(OperatorDepartmentDto.Contacts))]
    [MapProperty(nameof(OperatorDepartment.Rooms), nameof(OperatorDepartmentDto.Rooms), Use = nameof(MapRooms))]
    public static partial OperatorDepartmentDto MapOperatorDepartmentToDto(OperatorDepartment entity);

    private static OperatorDepartmentRoomInfoDto[] MapRooms(IEnumerable<Room> rooms)
    {
        return rooms.Select(t => new OperatorDepartmentRoomInfoDto
            {
                RoomId = t.Id,
                ScheduleAddress = t.ScheduleAddress?.Map(a => new ScheduleAddressDto
                {
                    RoomNumber = a.RoomNumber,
                    Address = a.Address,
                    ScheduleRoomId = a.ScheduleRoomId,
                })
            })
            .ToArray();
    }
}