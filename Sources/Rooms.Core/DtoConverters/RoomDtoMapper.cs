using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Room;
using Rooms.Core.Dtos.Room.Fix;
using Rooms.Core.Dtos.Room.Parameters;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Core.DtoConverters;

[Mapper]
public static partial class RoomDtoMapper
{
    public static partial RoomDto Convert(Room room);
    public static partial RoomType Convert(RoomTypeDto roomType);
    public static partial RoomLayout Convert(RoomLayoutDto roomLayout);
    public static partial RoomNetType Convert(RoomNetTypeDto roomNetType);
    public static partial RoomStatus Convert(RoomStatusDto roomStatus);
    public static partial Room Convert(RoomDto room);
}