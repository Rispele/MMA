using Commons;
using Commons.Optional;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Dtos.Room;
using WebApi.Models.Files;
using WebApi.Models.Room;

namespace WebApi.ModelConverters;

public static partial class RoomsModelsConverter
{
    public static RoomDto Convert(RoomModel model)
    {
        return new RoomDto
        (
            model.Id,
            model.Name,
            model.Description,
            Convert(model.ScheduleAddress),
            Convert(model.Parameters),
            Convert(model.Attachments),
            model.Owner,
            Convert(model.OperatorDepartment),
            Convert(model.FixStatus),
            model.AllowBooking,
            model.Equipments.Select(x => x.Map(EquipmentsModelsConverter.Convert))
        );
    }


    private static RoomTypeDto Convert(RoomTypeModel value)
    {
        return value switch
        {
            RoomTypeModel.Unspecified => RoomTypeDto.Unspecified,
            RoomTypeModel.Multimedia => RoomTypeDto.Multimedia,
            RoomTypeModel.Computer => RoomTypeDto.Computer,
            RoomTypeModel.Special => RoomTypeDto.Special,
            RoomTypeModel.Mixed => RoomTypeDto.Mixed,
            _ => RoomTypeDto.Unspecified
        };
    }

    private static RoomLayoutDto Convert(RoomLayoutModel value)
    {
        return value switch
        {
            RoomLayoutModel.Unspecified => RoomLayoutDto.Unspecified,
            RoomLayoutModel.Flat => RoomLayoutDto.Flat,
            RoomLayoutModel.Amphitheater => RoomLayoutDto.Amphitheater,
            _ => RoomLayoutDto.Unspecified
        };
    }

    private static RoomNetTypeDto Convert(RoomNetTypeModel value)
    {
        return value switch
        {
            RoomNetTypeModel.Unspecified => RoomNetTypeDto.Unspecified,
            RoomNetTypeModel.None => RoomNetTypeDto.None,
            RoomNetTypeModel.Wired => RoomNetTypeDto.Wired,
            RoomNetTypeModel.Wireless => RoomNetTypeDto.Wireless,
            RoomNetTypeModel.WiredAndWireless => RoomNetTypeDto.WiredAndWireless,
            _ => RoomNetTypeDto.Unspecified
        };
    }

    private static RoomStatusDto Convert(RoomStatusModel value)
    {
        return value switch
        {
            RoomStatusModel.Unspecified => RoomStatusDto.Unspecified,
            RoomStatusModel.Ready => RoomStatusDto.Ready,
            RoomStatusModel.PartiallyReady => RoomStatusDto.PartiallyReady,
            RoomStatusModel.NotReady => RoomStatusDto.NotReady,
            _ => RoomStatusDto.Unspecified
        };
    }

    private static ScheduleAddressDto? Convert(ScheduleAddressModel? model)
    {
        return model == null
            ? null
            : new ScheduleAddressDto(
                model.Address,
                model.RoomNumber
            );
    }

    private static RoomParametersDto Convert(RoomParametersModel model)
    {
        return new RoomParametersDto(
            Convert(model.Type),
            Convert(model.Layout),
            Convert(model.NetType),
            model.Seats,
            model.ComputerSeats,
            model.HasConditioning
        );
    }

    private static RoomOperatorDepartmentDto? Convert(RoomOperatorDepartmentModel? model)
    {
        return model == null
            ? null
            : new RoomOperatorDepartmentDto(
                model.Id,
                model.Name,
                model.Contacts,
                model.RoomOperator.Select(Convert).ToArray()
            );
    }

    private static RoomAttachmentsDto Convert(RoomAttachmentsModel model)
    {
        return new RoomAttachmentsDto(
            Convert(model.PdfRoomScheme),
            Convert(model.Photo)
        );
    }

    private static RoomFixStatusDto Convert(RoomFixStatusModel model)
    {
        return new RoomFixStatusDto(
            Convert(model.Status),
            model.FixDeadline,
            model.Comment
        );
    }

    private static RoomOperatorDto? Convert(RoomOperatorModel? model)
    {
        return model == null
            ? null
            : new RoomOperatorDto(
                model.Id,
                model.Name,
                model.UserId
            );
    }

    private static FileDescriptorDto? Convert(FileDescriptorModel? model)
    {
        return model.AsOptional().Map(t => new FileDescriptorDto(t.FileName, Convert(t.Location)));
    }

    private static FileLocationDto Convert(FileLocationModel model)
    {
        return new FileLocationDto(model.Id, model.Bucket);
    }
}