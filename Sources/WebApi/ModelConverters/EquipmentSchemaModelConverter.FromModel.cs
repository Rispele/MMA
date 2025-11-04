using Commons.Optional;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using WebApi.Models.Requests.EquipmentSchemas;

namespace WebApi.ModelConverters;

public static partial class EquipmentSchemaModelConverter
{
    public static GetEquipmentSchemasDto Convert(GetEquipmentSchemasModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new GetEquipmentSchemasDto(
            Math.Max(val1: 0, model.Page - 1),
            model.PageSize,
            model.AfterEquipmentSchemaId,
            model.Filter
                .AsOptional()
                .Map(filter => new EquipmentSchemasFilterDto
                {
                    Name = FilterModelMapper.MapFilterParameter(filter.Name, map: v => v),
                    EquipmentTypeName = FilterModelMapper.MapFilterParameter(filter.EquipmentTypeName, map: v => v),
                    EquipmentParameters = FilterModelMapper.MapFilterParameter(filter.EquipmentParameters, map: v => v)
                }));
    }
}