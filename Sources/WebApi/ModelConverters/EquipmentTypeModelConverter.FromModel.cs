using Commons.Optional;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using WebApi.Models.Requests.EquipmentTypes;

namespace WebApi.ModelConverters;

public static partial class EquipmentTypeModelConverter
{
    public static GetEquipmentTypesDto Convert(GetEquipmentTypesModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new GetEquipmentTypesDto(
            Math.Max(val1: 0, model.Page - 1),
            model.PageSize,
            model.AfterEquipmentTypeId,
            model.Filter
                .AsOptional()
                .Map(filter => new EquipmentTypesFilterDto
                {
                    Name = FilterModelMapper.MapFilterParameter(filter.Name, map: v => v)
                }));
    }
}