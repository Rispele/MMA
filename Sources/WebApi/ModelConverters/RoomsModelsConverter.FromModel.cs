using Commons.Optional;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Room;
using WebApi.Models.Files;
using WebApi.Models.Requests.Filtering;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Room;

namespace WebApi.ModelConverters;

public static partial class RoomsModelsConverter
{
    public static GetRoomsDto Convert(GetRoomsModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new GetRoomsDto(
            Math.Max(0, model.Page - 1),
            model.PageSize,
            model.AfterRoomId,
            model.Filter
                .AsOptional()
                .Map(filter => new RoomsFilterDto
                {
                    Name = MapFilterParameter(filter.Name, v => v),
                    Description = MapFilterParameter(filter.Description, v => v),
                    RoomTypes = MapFilterMultiParameter(filter.RoomTypes, Convert),
                    RoomLayout = MapFilterMultiParameter(filter.RoomLayout, Convert),
                    Seats = MapFilterParameter(filter.Seats, v => v),
                    ComputerSeats = MapFilterParameter(filter.ComputerSeats, v => v),
                    NetTypes = MapFilterMultiParameter(filter.NetTypes, Convert),
                    Conditioning = MapFilterParameter(filter.Conditioning, v => v),
                    Owner = MapFilterParameter(filter.Owner, v => v),
                    RoomStatuses = MapFilterMultiParameter(filter.RoomStatuses, Convert),
                    FixDeadline = MapFilterParameter(filter.FixDeadline, v => v),
                    Comment = MapFilterParameter(filter.Comment, v => v),
                }));
    }

    public static CreateRoomDto Convert(CreateRoomModel model)
    {
        return new CreateRoomDto
        {
            Name = model.Name,
            Description = model.Description,
            Type = Convert(model.Type),
            Layout = Convert(model.Layout),
            Seats = model.Seats,
            ComputerSeats = model.ComputerSeats,
            PdfRoomSchemeFileMetadata = Convert(model.PdfRoomSchemeFileMetadata),
            PhotoFileMetadata = Convert(model.PhotoFileMetadata),
            NetType = Convert(model.NetType),
            HasConditioning = model.HasConditioning,
            Owner = model.Owner,
            RoomStatus = Convert(model.RoomStatus),
            Comment = model.Comment,
            FixDeadline = model.FixDeadline,
            AllowBooking = model.AllowBooking
        };
    }

    public static PatchRoomDto Convert(PatchRoomModel patchModel)
    {
        return new PatchRoomDto
        {
            Name = patchModel.Name,
            Description = patchModel.Description,
            Type = Convert(patchModel.Type),
            Layout = Convert(patchModel.Layout),
            Seats = patchModel.Seats,
            ComputerSeats = patchModel.ComputerSeats,
            PdfRoomSchemeFileMetadata = null,
            PhotoFileMetadata = null,
            NetType = Convert(patchModel.NetType),
            HasConditioning = patchModel.HasConditioning,
            Owner = patchModel.Owner,
            RoomStatus = Convert(patchModel.RoomStatus),
            Comment = patchModel.Comment,
            FixDeadline = patchModel.FixDeadline,
            AllowBooking = patchModel.AllowBooking
        };
    }

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
            model.AllowBooking
        );
    }


    private static FilterParameterDto<TOut>? MapFilterParameter<TIn, TOut>(FilterParameterModel<TIn>? src, Func<TIn, TOut> map)
    {
        if (src == null || src.Value == null) return null;
        return new FilterParameterDto<TOut>(map(src.Value), Convert(src.SortDirection));
    }

    private static FilterMultiParameterDto<TOut>? MapFilterMultiParameter<TIn, TOut>(FilterMultiParameterModel<TIn>? src, Func<TIn, TOut> map)
    {
        if (src?.Values == null || src.Values.Length == 0) return null;
        return new FilterMultiParameterDto<TOut>(src.Values.Select(map).ToArray(), Convert(src.SortDirection));
    }

    private static SortDirectionDto Convert(SortDirectionModel direction)
    {
        return direction switch
        {
            SortDirectionModel.None => SortDirectionDto.None,
            SortDirectionModel.Ascending => SortDirectionDto.Ascending,
            SortDirectionModel.Descending => SortDirectionDto.Descending,
            _ => SortDirectionDto.None
        };
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
        return model == null ? null : new ScheduleAddressDto(
            model.Address,
            model.RoomNumber
        );
    }
    private static RoomParametersDto? Convert(RoomParametersModel? model)
    {
        return model == null ? null : new RoomParametersDto(
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
        return model == null ? null : new RoomOperatorDepartmentDto(
            model.Id,
            model.Name,
            model.Contacts,
            model.RoomOperator.Select(Convert).ToArray()
        );
    }

    private static RoomAttachmentsDto? Convert(RoomAttachmentsModel? model)
    {
        return model == null ? null : new RoomAttachmentsDto(
            Convert(model.PdfRoomScheme),
            Convert(model.Photo)
        );
    }

    private static RoomFixStatusDto? Convert(RoomFixStatusModel? model)
    {
        return model == null ? null : new RoomFixStatusDto(
            Convert(model.Status),
            model.FixDeadline,
            model.Comment
        );
    }

    private static RoomOperatorDto? Convert(RoomOperatorModel? model)
    {
        return model == null ? null : new RoomOperatorDto(
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