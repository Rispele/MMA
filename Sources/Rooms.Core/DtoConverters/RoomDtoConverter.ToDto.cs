using Commons;
using Commons.Optional;
using Rooms.Core.Dtos.Room;
using Rooms.Core.Dtos.Room.Fix;
using Rooms.Core.Dtos.Room.Parameters;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Core.DtoConverters;

public static partial class RoomDtoConverter
{
    public static RoomDto Convert(Room room)
    {
        return new RoomDto(
            room.Id,
            room.Name,
            room.Description,
            room.ScheduleAddress.Map(Convert),
            room.Parameters.Map(Convert),
            room.Attachments.Map(Convert),
            room.Owner,
            room.FixInfo.Map(Convert),
            room.AllowBooking,
            room.Equipments.IsNotEmpty() ? room.Equipments.Select(x => x.Map(EquipmentDtoConverter.Convert)) : [],
            room.OperatorRoomId);
    }

    private static ScheduleAddressDto? Convert(RoomScheduleAddress? entity)
    {
        return entity
            .AsOptional()
            .Map(scheduleAddress => new ScheduleAddressDto(scheduleAddress.RoomNumber, scheduleAddress.Address));
    }

    private static RoomParametersDto Convert(RoomParameters parameters)
    {
        return new RoomParametersDto(
            parameters.Type.Map(Convert),
            parameters.Layout.Map(Convert),
            parameters.NetType.Map(Convert),
            parameters.Seats,
            parameters.ComputerSeats,
            parameters.HasConditioning);
    }

    private static RoomTypeDto Convert(RoomType roomType)
    {
        return roomType switch
        {
            RoomType.Unspecified => RoomTypeDto.Unspecified,
            RoomType.Multimedia => RoomTypeDto.Multimedia,
            RoomType.Computer => RoomTypeDto.Computer,
            RoomType.Special => RoomTypeDto.Special,
            RoomType.Mixed => RoomTypeDto.Mixed,
            _ => throw new ArgumentOutOfRangeException(nameof(roomType), roomType, message: null)
        };
    }

    private static RoomLayoutDto Convert(RoomLayout roomLayout)
    {
        return roomLayout switch
        {
            RoomLayout.Unspecified => RoomLayoutDto.Unspecified,
            RoomLayout.Flat => RoomLayoutDto.Flat,
            RoomLayout.Amphitheater => RoomLayoutDto.Amphitheater,
            _ => throw new ArgumentOutOfRangeException(nameof(roomLayout), roomLayout, message: null)
        };
    }

    private static RoomNetTypeDto Convert(RoomNetType roomNetType)
    {
        return roomNetType switch
        {
            RoomNetType.Unspecified => RoomNetTypeDto.Unspecified,
            RoomNetType.None => RoomNetTypeDto.None,
            RoomNetType.Wired => RoomNetTypeDto.Wired,
            RoomNetType.Wireless => RoomNetTypeDto.Wireless,
            RoomNetType.WiredAndWireless => RoomNetTypeDto.WiredAndWireless,
            _ => throw new ArgumentOutOfRangeException(nameof(roomNetType), roomNetType, message: null)
        };
    }

    private static RoomAttachmentsDto Convert(RoomAttachments attachments)
    {
        return new RoomAttachmentsDto(
            attachments.PdfRoomScheme.AsOptional().Map(FileConvertExtensions.ToDto),
            attachments.Photo.AsOptional().Map(FileConvertExtensions.ToDto));
    }

    private static RoomFixStatusDto Convert(RoomFixInfo fixInfo)
    {
        return new RoomFixStatusDto(
            fixInfo.Status.Map(Convert),
            fixInfo.FixDeadline,
            fixInfo.Comment);
    }

    private static RoomStatusDto Convert(RoomStatus roomStatus)
    {
        return roomStatus switch
        {
            RoomStatus.Unspecified => RoomStatusDto.Unspecified,
            RoomStatus.Ready => RoomStatusDto.Ready,
            RoomStatus.PartiallyReady => RoomStatusDto.PartiallyReady,
            RoomStatus.NotReady => RoomStatusDto.NotReady,
            _ => throw new ArgumentOutOfRangeException(nameof(roomStatus), roomStatus, message: null)
        };
    }
}