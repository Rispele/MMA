using Commons.Optional;
using Rooms.Core.Dtos.Requests.Equipments;
using WebApi.Models.Requests.Equipments;

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
                    RoomName = FilterModelMapper.MapFilterMultiParameter(filter.RoomName, map: v => v),
                    Types = FilterModelMapper.MapFilterMultiParameter(filter.Types, EquipmentTypeModelMapper.MapEquipmentTypeFromModel),
                    Schemas = FilterModelMapper.MapFilterMultiParameter(filter.Schemas, EquipmentSchemaModelMapper.MapEquipmentSchemaFromModel),
                    InventoryNumber = FilterModelMapper.MapFilterParameter(filter.InventoryNumber, map: v => v),
                    SerialNumber = FilterModelMapper.MapFilterParameter(filter.SerialNumber, map: v => v),
                    NetworkEquipmentIp = FilterModelMapper.MapFilterParameter(filter.NetworkEquipmentIp, map: v => v),
                    Comment = FilterModelMapper.MapFilterParameter(filter.Comment, map: v => v),
                    Statuses = FilterModelMapper.MapFilterMultiParameter(filter.Statuses, map: v => v)
                }));
    }
}