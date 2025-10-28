using Commons;
using Commons.Optional;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Requests.Filtering;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Requests.Filtering;

namespace WebApi.ModelConverters;

public static partial class EquipmentsModelsConverter
{
    public static GetEquipmentsDto Convert(GetEquipmentsModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new GetEquipmentsDto(
            Math.Max(0, model.Page - 1),
            model.PageSize,
            model.AfterEquipmentId,
            model.Filter
                .AsOptional()
                .Map(filter => new EquipmentsFilterDto
                {
                    RoomName = MapFilterMultiParameter(filter.RoomName, v => v),
                    Types = MapFilterMultiParameter(filter.Types, EquipmentTypesModelsConverter.Convert),
                    Schemas = MapFilterMultiParameter(filter.Schemas, EquipmentSchemasModelsConverter.Convert),
                    InventoryNumber = MapFilterParameter(filter.InventoryNumber, v => v),
                    SerialNumber = MapFilterParameter(filter.SerialNumber, v => v),
                    NetworkEquipmentIp = MapFilterParameter(filter.NetworkEquipmentIp, v => v),
                    Comment = MapFilterParameter(filter.Comment, v => v),
                    Statuses = MapFilterMultiParameter(filter.Statuses, v => v)
                }));
    }

    public static CreateEquipmentDto Convert(CreateEquipmentModel model)
    {
        return new CreateEquipmentDto
        {
            RoomId = model.RoomId,
            SchemaId = model.SchemaId,
            InventoryNumber = model.InventoryNumber,
            SerialNumber = model.SerialNumber,
            NetworkEquipmentIp = model.NetworkEquipmentIp,
            Comment = model.Comment,
            Status = model.Status
        };
    }

    public static PatchEquipmentDto Convert(PatchEquipmentModel patchModel)
    {
        return new PatchEquipmentDto();
    }

    private static FilterParameterDto<TOut>? MapFilterParameter<TIn, TOut>(FilterParameterModel<TIn>? src,
        Func<TIn, TOut> map)
    {
        if (src == null || src.Value == null) return null;
        return new FilterParameterDto<TOut>(map(src.Value), Convert(src.SortDirection));
    }

    private static FilterMultiParameterDto<TOut>? MapFilterMultiParameter<TIn, TOut>(
        FilterMultiParameterModel<TIn>? src, Func<TIn, TOut> map)
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

    public static EquipmentDto Convert(EquipmentModel equipment)
    {
        return new EquipmentDto
        {
            Id = equipment.Id,
            Room = equipment.RoomModel.Map(RoomsModelsConverter.Convert),
            SchemaDto = equipment.SchemaModel.Map(EquipmentSchemasModelsConverter.Convert),
            InventoryNumber = equipment.InventoryNumber,
            SerialNumber = equipment.SerialNumber,
            NetworkEquipmentIp = equipment.NetworkEquipmentIp,
            Comment = equipment.Comment,
            Status = equipment.Status
        };
    }
}