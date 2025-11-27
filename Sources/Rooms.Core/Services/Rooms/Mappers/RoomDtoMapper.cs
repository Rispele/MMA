using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Interfaces.Dtos.Room.Fix;
using Rooms.Core.Interfaces.Dtos.Room.Parameters;
using Rooms.Domain.Models.Rooms;
using Rooms.Domain.Propagated.Rooms;

namespace Rooms.Core.Services.Rooms.Mappers;

[Mapper]
public static partial class RoomDtoMapper
{
    public static partial RoomDto Map(Room room);
    public static partial RoomType Map(RoomTypeDto roomType);
    public static partial RoomLayout Map(RoomLayoutDto roomLayout);
    public static partial RoomNetType Map(RoomNetTypeDto roomNetType);
    public static partial RoomStatus Map(RoomStatusDto roomStatus);
}