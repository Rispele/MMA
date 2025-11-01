using Commons;
using Commons.Optional;
using Rooms.Core.Dtos.Room;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Core.DtoConverters;

public static partial class RoomDtoConverter
{
    public static Room Convert(RoomDto room)
    {
        return new Room
        {
            Id = room.Id,
            Name = room.Name,
            Description = room.Description,
            ScheduleAddress = room.ScheduleAddress.Map(Convert),
            Parameters = room.Parameters.Map(Convert),
            Attachments = room.Attachments.Map(Convert),
            Owner = room.Owner,
            FixInfo = room.FixStatus.Map(Convert),
            AllowBooking = room.AllowBooking
        };
    }

    public static RoomType Convert(RoomTypeDto roomType)
    {
        return roomType switch
        {
            RoomTypeDto.Unspecified => RoomType.Unspecified,
            RoomTypeDto.Multimedia => RoomType.Multimedia,
            RoomTypeDto.Computer => RoomType.Computer,
            RoomTypeDto.Special => RoomType.Special,
            RoomTypeDto.Mixed => RoomType.Mixed,
            _ => throw new ArgumentOutOfRangeException(nameof(roomType), roomType, message: null)
        };
    }

    public static RoomLayout Convert(RoomLayoutDto roomLayout)
    {
        return roomLayout switch
        {
            RoomLayoutDto.Unspecified => RoomLayout.Unspecified,
            RoomLayoutDto.Flat => RoomLayout.Flat,
            RoomLayoutDto.Amphitheater => RoomLayout.Amphitheater,
            _ => throw new ArgumentOutOfRangeException(nameof(roomLayout), roomLayout, message: null)
        };
    }

    public static RoomNetType Convert(RoomNetTypeDto roomNetType)
    {
        return roomNetType switch
        {
            RoomNetTypeDto.Unspecified => RoomNetType.Unspecified,
            RoomNetTypeDto.None => RoomNetType.None,
            RoomNetTypeDto.Wired => RoomNetType.Wired,
            RoomNetTypeDto.Wireless => RoomNetType.Wireless,
            RoomNetTypeDto.WiredAndWireless => RoomNetType.WiredAndWireless,
            _ => throw new ArgumentOutOfRangeException(nameof(roomNetType), roomNetType, message: null)
        };
    }

    public static RoomStatus Convert(RoomStatusDto roomStatus)
    {
        return roomStatus switch
        {
            RoomStatusDto.Unspecified => RoomStatus.Unspecified,
            RoomStatusDto.Ready => RoomStatus.Ready,
            RoomStatusDto.PartiallyReady => RoomStatus.PartiallyReady,
            RoomStatusDto.NotReady => RoomStatus.NotReady,
            _ => throw new ArgumentOutOfRangeException(nameof(roomStatus), roomStatus, message: null)
        };
    }

    private static RoomScheduleAddress? Convert(ScheduleAddressDto? entity)
    {
        return entity
            .AsOptional()
            .Map(scheduleAddress => new RoomScheduleAddress(scheduleAddress.RoomNumber, scheduleAddress.Address));
    }

    private static RoomParameters Convert(RoomParametersDto parameters)
    {
        return new RoomParameters
        {
            Type = parameters.Type.Map(Convert),
            Layout = parameters.Layout.Map(Convert),
            NetType = parameters.NetType.Map(Convert),
            Seats = parameters.Seats,
            ComputerSeats = parameters.ComputerSeats,
            HasConditioning = parameters.HasConditioning
        };
    }

    private static RoomAttachments Convert(RoomAttachmentsDto attachments)
    {
        return new RoomAttachments
        {
            PdfRoomScheme = attachments.PdfRoomScheme.AsOptional().Map(FileConvertExtensions.FromDto),
            Photo = attachments.Photo.AsOptional().Map(FileConvertExtensions.FromDto),
        };
    }

    private static RoomFixInfo Convert(RoomFixStatusDto fixStatus)
    {
        return new RoomFixInfo
        {
            Status = fixStatus.Status.Map(Convert),
            FixDeadline = fixStatus.FixDeadline,
            Comment = fixStatus.Comment,
        };
    }
}