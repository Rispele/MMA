using Commons.Optional;
using Rooms.Core.Dtos.OperatorRoom;
using Rooms.Core.Dtos.Requests.OperatorRooms;
using Rooms.Core.Dtos.Requests.Filtering;
using WebApi.Models.OperatorRoom;
using WebApi.Models.Requests.OperatorRooms;
using WebApi.Models.Requests.Filtering;

namespace WebApi.ModelConverters;

public static partial class OperatorRoomsModelsConverter
{
    public static GetOperatorRoomsDto Convert(GetOperatorRoomsModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new GetOperatorRoomsDto(
            Math.Max(val1: 0, model.Page - 1),
            model.PageSize,
            model.AfterOperatorRoomId,
            model.Filter
                .AsOptional()
                .Map(filter => new OperatorRoomsFilterDto
                {
                    Name = MapFilterParameter(filter.Name, v => v),
                    RoomName = MapFilterParameter(filter.RoomName, v => v),
                    Operator = MapFilterParameter(filter.Operator, v => v),
                    OperatorEmail = MapFilterParameter(filter.OperatorEmail, v => v),
                    Contacts = MapFilterParameter(filter.Contacts, v => v),
                }));
    }

    public static CreateOperatorRoomDto Convert(CreateOperatorRoomModel model)
    {
        return new CreateOperatorRoomDto
        {
            Name = model.Name,
            RoomIds = model.RoomIds,
            Operators = model.Operators,
            Contacts = model.Contacts,
        };
    }

    public static PatchOperatorRoomDto Convert(PatchOperatorRoomModel patchModel)
    {
        return null!;
    }

    private static FilterParameterDto<TOut>? MapFilterParameter<TIn, TOut>(
        FilterParameterModel<TIn>? src,
        Func<TIn, TOut> map)
    {
        if (src == null || src.Value == null)
        {
            return null;
        }

        return new FilterParameterDto<TOut>(map(src.Value), Convert(src.SortDirection));
    }

    private static FilterMultiParameterDto<TOut>? MapFilterMultiParameter<TIn, TOut>(
        FilterMultiParameterModel<TIn>? src,
        Func<TIn, TOut> map)
    {
        if (src?.Values == null || src.Values.Length == 0)
        {
            return null;
        }

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

    public static OperatorRoomDto Convert(OperatorRoomModel entity)
    {
        return new OperatorRoomDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Rooms = entity.Rooms,
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }
}