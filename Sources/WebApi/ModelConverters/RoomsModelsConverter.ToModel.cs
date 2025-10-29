using Rooms.Core.Dtos.Files;
using Rooms.Core.Dtos.Room;
using WebApi.Models.Files;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Room;

namespace WebApi.ModelConverters;

public static partial class RoomsModelsConverter
{
    public static RoomModel Convert(RoomDto dto)
    {
        return new RoomModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            ScheduleAddress = Convert(dto.ScheduleAddress),
            Parameters = Convert(dto.Parameters),
            Attachments = Convert(dto.Attachments),
            Owner = dto.Owner,
            OperatorDepartment = Convert(dto.OperatorDepartment),
            FixStatus = Convert(dto.FixStatus),
            AllowBooking = dto.AllowBooking
        };
    }

    public static PatchRoomModel ConvertToPatchModel(RoomDto dto)
    {
        return new PatchRoomModel
        {
            Name = dto.Name,
            Description = dto.Description,
            Type = Convert(dto.Parameters.Type),
            Layout = Convert(dto.Parameters.Layout),
            NetType = Convert(dto.Parameters.NetType),
            Seats = dto.Parameters.Seats,
            ComputerSeats = dto.Parameters.ComputerSeats,
            HasConditioning = dto.Parameters.HasConditioning,
            Owner = dto.Owner,
            RoomStatus = Convert(dto.FixStatus.Status),
            FixDeadline = dto.FixStatus.FixDeadline,
            Comment = dto.FixStatus.Comment,
            AllowBooking = dto.AllowBooking
        };
    }

    private static ScheduleAddressModel? Convert(ScheduleAddressDto? dto)
    {
        return dto == null
            ? null
            : new ScheduleAddressModel
            {
                RoomNumber = dto.RoomNumber,
                Address = dto.Address
            };
    }

    private static RoomParametersModel Convert(RoomParametersDto dto)
    {
        return new RoomParametersModel
        {
            Type = Convert(dto.Type),
            Layout = Convert(dto.Layout),
            NetType = Convert(dto.NetType),
            Seats = dto.Seats,
            ComputerSeats = dto.ComputerSeats,
            HasConditioning = dto.HasConditioning
        };
    }

    private static RoomAttachmentsModel Convert(RoomAttachmentsDto dto)
    {
        return new RoomAttachmentsModel(
            Convert(dto.PdfRoomScheme),
            Convert(dto.Photo));
    }

    private static FileDescriptorModel? Convert(FileDescriptorDto? dto)
    {
        return dto == null ? null : new FileDescriptorModel(dto.FileName, Convert(dto.FileLocation));
    }

    private static FileLocationModel Convert(FileLocationDto dto)
    {
        return new FileLocationModel(dto.Id, dto.Bucket);
    }

    private static RoomOperatorDepartmentModel? Convert(RoomOperatorDepartmentDto? dto)
    {
        if (dto == null)
        {
            return null;
        }

        return new RoomOperatorDepartmentModel(
            dto.Id,
            dto.Name,
            dto.Contacts,
            dto.RoomOperator.Select(Convert).ToArray());
    }

    private static RoomOperatorModel Convert(RoomOperatorDto dto)
    {
        return new RoomOperatorModel(dto.Id, dto.Name, dto.UserId);
    }

    private static RoomFixStatusModel Convert(RoomFixStatusDto dto)
    {
        return new RoomFixStatusModel(Convert(dto.Status), dto.FixDeadline, dto.Comment);
    }

    private static RoomTypeModel Convert(RoomTypeDto value)
    {
        return value switch
        {
            RoomTypeDto.Unspecified => RoomTypeModel.Unspecified,
            RoomTypeDto.Multimedia => RoomTypeModel.Multimedia,
            RoomTypeDto.Computer => RoomTypeModel.Computer,
            RoomTypeDto.Special => RoomTypeModel.Special,
            RoomTypeDto.Mixed => RoomTypeModel.Mixed,
            _ => RoomTypeModel.Unspecified
        };
    }

    private static RoomLayoutModel Convert(RoomLayoutDto value)
    {
        return value switch
        {
            RoomLayoutDto.Unspecified => RoomLayoutModel.Unspecified,
            RoomLayoutDto.Flat => RoomLayoutModel.Flat,
            RoomLayoutDto.Amphitheater => RoomLayoutModel.Amphitheater,
            _ => RoomLayoutModel.Unspecified
        };
    }

    private static RoomNetTypeModel Convert(RoomNetTypeDto value)
    {
        return value switch
        {
            RoomNetTypeDto.Unspecified => RoomNetTypeModel.Unspecified,
            RoomNetTypeDto.None => RoomNetTypeModel.None,
            RoomNetTypeDto.Wired => RoomNetTypeModel.Wired,
            RoomNetTypeDto.Wireless => RoomNetTypeModel.Wireless,
            RoomNetTypeDto.WiredAndWireless => RoomNetTypeModel.WiredAndWireless,
            _ => RoomNetTypeModel.Unspecified
        };
    }

    private static RoomStatusModel Convert(RoomStatusDto value)
    {
        return value switch
        {
            RoomStatusDto.Unspecified => RoomStatusModel.Unspecified,
            RoomStatusDto.Ready => RoomStatusModel.Ready,
            RoomStatusDto.PartiallyReady => RoomStatusModel.PartiallyReady,
            RoomStatusDto.NotReady => RoomStatusModel.NotReady,
            _ => RoomStatusModel.Unspecified
        };
    }
}