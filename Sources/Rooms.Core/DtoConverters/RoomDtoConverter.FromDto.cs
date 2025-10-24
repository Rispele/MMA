using Commons;
using Commons.Optional;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Room;
using Rooms.Domain.Models.RoomModels;
using Rooms.Domain.Models.RoomModels.Fix;
using Rooms.Domain.Models.RoomModels.Parameters;
using Rooms.Domain.Queries.Implementations.Rooms;

namespace Rooms.Core.DtoConverters;

public static partial class RoomDtoConverter
{
    public static Room Convert(RoomDto room)
    {
        return new Room(
            room.Name,
            room.Description,
            room.ScheduleAddress.Map(Convert),
            room.Parameters.Map(Convert),
            room.Attachments.Map(Convert),
            room.Owner,
            room.FixStatus.Map(Convert),
            room.AllowBooking);
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
            _ => throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null)
        };
    }

    public static RoomLayout Convert(RoomLayoutDto roomLayout)
    {
        return roomLayout switch
        {
            RoomLayoutDto.Unspecified => RoomLayout.Unspecified,
            RoomLayoutDto.Flat => RoomLayout.Flat,
            RoomLayoutDto.Amphitheater => RoomLayout.Amphitheater,
            _ => throw new ArgumentOutOfRangeException(nameof(roomLayout), roomLayout, null)
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
            _ => throw new ArgumentOutOfRangeException(nameof(roomNetType), roomNetType, null)
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
            _ => throw new ArgumentOutOfRangeException(nameof(roomStatus), roomStatus, null)
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
        return new RoomParameters(
            parameters.Type.Map(Convert),
            parameters.Layout.Map(Convert),
            parameters.NetType.Map(Convert),
            parameters.Seats,
            parameters.ComputerSeats,
            parameters.HasConditioning);
    }

    private static RoomAttachments Convert(RoomAttachmentsDto attachments)
    {
        return new RoomAttachments(
            attachments.PdfRoomScheme.AsOptional().Map(FileConvertExtensions.FromDto),
            attachments.Photo.AsOptional().Map(FileConvertExtensions.FromDto));
    }

    private static RoomFixInfo Convert(RoomFixStatusDto fixStatus)
    {
        return new RoomFixInfo(
            fixStatus.Status.Map(Convert),
            fixStatus.FixDeadline,
            fixStatus.Comment);
    }
}