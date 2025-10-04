using Application.Implementations.Dtos.Room;
using Commons;
using Commons.Optional;
using Domain.Models.Room;
using Domain.Models.Room.Fix;
using Domain.Models.Room.Parameters;

namespace Application.Implementations.Services.DtoConverters;

public partial class RoomDtoConverter
{
    public RoomDto Convert(Room room)
    {
        return new RoomDto(
            room.Id,
            room.Name,
            room.Description,
            room.ScheduleAddress.Map(Convert),
            room.Parameters.Map(Convert),
            room.Attachments.Map(Convert),
            room.Owner,
            operatorDepartment: null,
            room.FixInfo.Map(Convert),
            room.AllowBooking);
    }

    private static ScheduleAddressDto? Convert(RoomScheduleAddress? entity)
    {
        return entity
            .AsOptional()
            .Map(scheduleAddress => new ScheduleAddressDto(scheduleAddress.RoomNumber, scheduleAddress.Address));
    }

    private RoomParametersDto Convert(RoomParameters parameters)
    {
        return new RoomParametersDto(
            parameters.Type.Map(Convert),
            parameters.Layout.Map(Convert),
            parameters.NetType.Map(Convert),
            parameters.Seats,
            parameters.ComputerSeats,
            parameters.HasConditioning);
    }

    private RoomTypeDto Convert(RoomType roomType)
    {
        return roomType switch
        {
            RoomType.Unspecified => RoomTypeDto.Unspecified,
            RoomType.Multimedia => RoomTypeDto.Multimedia,
            RoomType.Computer => RoomTypeDto.Computer,
            RoomType.Special => RoomTypeDto.Special,
            RoomType.Mixed => RoomTypeDto.Mixed,
            _ => throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null)
        };
    }

    private RoomLayoutDto Convert(RoomLayout roomLayout)
    {
        return roomLayout switch
        {
            RoomLayout.Unspecified => RoomLayoutDto.Unspecified,
            RoomLayout.Flat => RoomLayoutDto.Flat,
            RoomLayout.Amphitheater => RoomLayoutDto.Amphitheater,
            _ => throw new ArgumentOutOfRangeException(nameof(roomLayout), roomLayout, null)
        };
    }

    private RoomNetTypeDto Convert(RoomNetType roomNetType)
    {
        return roomNetType switch
        {
            RoomNetType.Unspecified => RoomNetTypeDto.Unspecified,
            RoomNetType.None => RoomNetTypeDto.None,
            RoomNetType.Wired => RoomNetTypeDto.Wired,
            RoomNetType.Wireless => RoomNetTypeDto.Wireless,
            RoomNetType.WiredAndWireless => RoomNetTypeDto.WiredAndWireless,
            _ => throw new ArgumentOutOfRangeException(nameof(roomNetType), roomNetType, null)
        };
    }
    
    private RoomAttachmentsDto Convert(RoomAttachments attachments)
    {
        return new RoomAttachmentsDto(
            attachments.PdfRoomScheme.AsOptional().Map(FileConvertExtensions.ToDto),
            attachments.Photo.AsOptional().Map(FileConvertExtensions.ToDto));
    }
    
    private RoomFixInfoDto Convert(RoomFixInfo fixInfo)
    {
        return new RoomFixInfoDto(
            fixInfo.Status.Map(Convert),
            fixInfo.FixDeadline,
            fixInfo.Comment);
    }

    private RoomStatusDto Convert(RoomStatus roomStatus)
    {
        return roomStatus switch
        {
            RoomStatus.Unspecified => RoomStatusDto.Unspecified,
            RoomStatus.Ready => RoomStatusDto.Ready,
            RoomStatus.PartiallyReady => RoomStatusDto.PartiallyReady,
            RoomStatus.NotReady => RoomStatusDto.NotReady,
            _ => throw new ArgumentOutOfRangeException(nameof(roomStatus), roomStatus, null)
        };
    }
}