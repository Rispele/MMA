using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Room;
using Rooms.Domain.Models.Room;

namespace Rooms.Core.DtoConverters;

[Mapper]
public static partial class RoomDtoConverter
{
    public static partial RoomDto Convert(Room room);
}