using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Domain.Queries.Implementations.Equipment;
using Rooms.Domain.Queries.Implementations.Filtering;
using Rooms.Domain.Queries.Implementations.Room;

namespace Rooms.Core.DtoConverters;

public static class FiltersDtoConverter
{
    public static EquipmentsFilter Convert(EquipmentsFilterDto filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        return new EquipmentsFilter
        {
            RoomName = MapFilterMultiParameter(filter.RoomName, v => v),
            Types = MapFilterMultiParameter(filter.Types, EquipmentDtoConverter.Convert),
            Schemas = MapFilterMultiParameter(filter.Schemas, EquipmentDtoConverter.Convert),
            InventoryNumber = MapFilterParameter(filter.InventoryNumber, v => v),
            SerialNumber = MapFilterParameter(filter.SerialNumber, v => v),
            NetworkEquipmentIp = MapFilterParameter(filter.NetworkEquipmentIp, v => v),
            Comment = MapFilterParameter(filter.Comment, v => v),
            Statuses = MapFilterMultiParameter(filter.Statuses, v => v),
        };
    }
    
    public static RoomsFilter Convert(RoomsFilterDto filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        return new RoomsFilter
        {
            Name = MapFilterParameter(filter.Name, v => v),
            Description = MapFilterParameter(filter.Description, v => v),
            RoomTypes = MapFilterMultiParameter(filter.RoomTypes, RoomDtoConverter.Convert),
            RoomLayout = MapFilterMultiParameter(filter.RoomLayout, RoomDtoConverter.Convert),
            Seats = MapFilterParameter(filter.Seats, v => v),
            ComputerSeats = MapFilterParameter(filter.ComputerSeats, v => v),
            NetTypes = MapFilterMultiParameter(filter.NetTypes, RoomDtoConverter.Convert),
            Conditioning = MapFilterParameter(filter.Conditioning, v => v),
            Owner = MapFilterParameter(filter.Owner, v => v),
            RoomStatuses = MapFilterMultiParameter(filter.RoomStatuses, RoomDtoConverter.Convert),
            FixDeadline = MapFilterParameter(filter.FixDeadline, v => v),
            Comment = MapFilterParameter(filter.Comment, v => v),
        };
    }
    
    private static FilterParameter<TOut>? MapFilterParameter<TIn, TOut>(FilterParameterDto<TIn>? src, Func<TIn, TOut> map)
    {
        if (src == null || src.Value == null) return null;
        return new FilterParameter<TOut>(map(src.Value), Convert(src.SortDirection));
    }

    private static FilterMultiParameter<TOut>? MapFilterMultiParameter<TIn, TOut>(FilterMultiParameterDto<TIn>? src, Func<TIn, TOut> map)
    {
        if (src?.Values == null || src.Values.Length == 0) return null;
        return new FilterMultiParameter<TOut>(src.Values.Select(map).ToArray(), Convert(src.SortDirection));
    }

    private static SortDirection Convert(SortDirectionDto direction)
    {
        return direction switch
        {
            SortDirectionDto.None => SortDirection.None,
            SortDirectionDto.Ascending => SortDirection.Ascending,
            SortDirectionDto.Descending => SortDirection.Descending,
            _ => SortDirection.None
        };
    }
}