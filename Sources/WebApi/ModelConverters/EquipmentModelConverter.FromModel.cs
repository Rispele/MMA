using Commons.Optional;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Requests.Filtering;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Requests.Filtering;

namespace WebApi.ModelConverters;

public static partial class EquipmentModelConverter
{
    public static GetEquipmentsDto Convert(GetEquipmentsModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new GetEquipmentsDto(
            Math.Max(val1: 0, model.Page - 1),
            model.PageSize,
            model.AfterEquipmentId,
            model.Filter
                .AsOptional()
                .Map(filter => new EquipmentsFilterDto
                {
                    RoomName = MapFilterMultiParameter(filter.RoomName, map: v => v),
                    Types = MapFilterMultiParameter(filter.Types, EquipmentTypeModelMapper.MapEquipmentTypeFromModel),
                    Schemas = MapFilterMultiParameter(filter.Schemas, EquipmentSchemaModelMapper.MapEquipmentSchemaFromModel),
                    InventoryNumber = MapFilterParameter(filter.InventoryNumber, map: v => v),
                    SerialNumber = MapFilterParameter(filter.SerialNumber, map: v => v),
                    NetworkEquipmentIp = MapFilterParameter(filter.NetworkEquipmentIp, map: v => v),
                    Comment = MapFilterParameter(filter.Comment, map: v => v),
                    Statuses = MapFilterMultiParameter(filter.Statuses, map: v => v)
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
}