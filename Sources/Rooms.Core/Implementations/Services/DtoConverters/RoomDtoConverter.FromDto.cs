using Rooms.Core.Implementations.Dtos.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Core.Implementations.Services.DtoConverters;

public partial class RoomDtoConverter
{
    public RoomType Convert(RoomTypeDto roomType)
    {
        return roomType switch
        {
            RoomTypeDto.Unspecified => RoomType.Unspecified,
            RoomTypeDto.Multimedia => RoomType.Multimedia,
            RoomTypeDto.Computer => RoomType.Computer,
            RoomTypeDto.Special => RoomType.Special,
            RoomTypeDto.Mixed => RoomType.Mixed,
            _ => throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null)
        };
    }

    public RoomLayout Convert(RoomLayoutDto roomLayout)
    {
        return roomLayout switch
        {
            RoomLayoutDto.Unspecified => RoomLayout.Unspecified,
            RoomLayoutDto.Flat => RoomLayout.Flat,
            RoomLayoutDto.Amphitheater => RoomLayout.Amphitheater,
            _ => throw new ArgumentOutOfRangeException(nameof(roomLayout), roomLayout, null)
        };
    }

    public RoomNetType Convert(RoomNetTypeDto roomNetType)
    {
        return roomNetType switch
        {
            RoomNetTypeDto.Unspecified => RoomNetType.Unspecified,
            RoomNetTypeDto.None => RoomNetType.None,
            RoomNetTypeDto.Wired => RoomNetType.Wired,
            RoomNetTypeDto.Wireless => RoomNetType.Wireless,
            RoomNetTypeDto.WiredAndWireless => RoomNetType.WiredAndWireless,
            _ => throw new ArgumentOutOfRangeException(nameof(roomNetType), roomNetType, null)
        };
    }

    public RoomStatus Convert(RoomStatusDto roomStatus)
    {
        return roomStatus switch
        {
            RoomStatusDto.Unspecified => RoomStatus.Unspecified,
            RoomStatusDto.Ready => RoomStatus.Ready,
            RoomStatusDto.PartiallyReady => RoomStatus.PartiallyReady,
            RoomStatusDto.NotReady => RoomStatus.NotReady,
            _ => throw new ArgumentOutOfRangeException(nameof(roomStatus), roomStatus, null)
        };
    }
}