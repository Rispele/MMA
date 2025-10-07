using Commons.Optional;
using Rooms.Core.Implementations.Dtos.Requests.Filtering;
using Rooms.Core.Implementations.Dtos.Requests.RoomsQuerying;
using Rooms.Core.Implementations.Dtos.Room;
using Rooms.Core.Implementations.Dtos.Requests.RoomCreating;
using Rooms.Core.Implementations.Dtos.Files;
using Rooms.Core.Implementations.Dtos.Requests.RoomPatching;
using WebApi.Models.Requests;
using WebApi.Models.Requests.Filtering;
using WebApi.Models.Room;
using WebApi.Models.Files;

namespace WebApi.Services.Implementations;

public partial class RoomsModelsConverter
{
    public GetRoomsRequestDto Convert(RoomsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new GetRoomsRequestDto
        {
            BatchNumber = Math.Max(0, request.Page - 1),
            BatchSize = request.PageSize,
            AfterRoomId = request.AfterRoomId,
            Filter = request.Filter
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
                })
        };
    }

    public CreateRoomRequest Convert(PostRoomRequest request)
    {
        return new CreateRoomRequest
        {
            Name = request.Name,
            Description = request.Description,
            Type = Convert(request.Type),
            Layout = Convert(request.Layout),
            Seats = request.Seats,
            ComputerSeats = request.ComputerSeats,
            PdfRoomSchemeFileMetadata = Convert(request.PdfRoomSchemeFileMetadata),
            PhotoFileMetadata = Convert(request.PhotoFileMetadata),
            NetType = Convert(request.NetType),
            HasConditioning = request.HasConditioning,
            Owner = request.Owner,
            RoomStatus = Convert(request.RoomStatus),
            Comment = request.Comment,
            FixDeadline = request.FixDeadline,
            AllowBooking = request.AllowBooking
        };
    }

    public PatchRoomRequest Convert(PatchRoomModel patchModel)
    {
        return new PatchRoomRequest
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


    private static FilterParameterDto<TOut>? MapFilterParameter<TIn, TOut>(FilterParameterModel<TIn>? src, Func<TIn, TOut> map)
    {
        if (src == null || src.Value == null) return null;
        return new FilterParameterDto<TOut>
        {
            Value = map(src.Value),
            SortDirectionDto = Convert(src.SortDirection)
        };
    }

    private static FilterMultiParameterDto<TOut>? MapFilterMultiParameter<TIn, TOut>(FilterMultiParameterModel<TIn>? src, Func<TIn, TOut> map)
    {
        if (src?.Values == null || src.Values.Length == 0) return null;
        return new FilterMultiParameterDto<TOut>
        {
            Values = src.Values.Select(map).ToArray(),
            SortDirectionDto = Convert(src.SortDirection)
        };
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

    private static FileMetadataDto? Convert(FileMetadataModel? model)
    {
        return model.AsOptional().Map(t => new FileMetadataDto(t.FileName, Convert(t.Location)));
    }

    private static FileLocationDto Convert(FileLocationModel model)
    {
        return new FileLocationDto(model.Id, model.Bucket);
    }
}