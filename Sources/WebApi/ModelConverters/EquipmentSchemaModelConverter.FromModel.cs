using Commons.Optional;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Requests.Filtering;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Requests.Filtering;

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
                    Name = MapFilterParameter(filter.Name, map: v => v),
                    EquipmentTypeName = MapFilterParameter(filter.EquipmentTypeName, map: v => v),
                    EquipmentParameters = MapFilterParameter(filter.EquipmentParameters, map: v => v)
                }));
    }

    public static CreateEquipmentSchemaDto Convert(CreateEquipmentSchemaModel model)
    {
        return new CreateEquipmentSchemaDto
        {
            Name = model.Name,
            EquipmentTypeId = model.EquipmentTypeId,
            ParameterValues = model.ParameterValues
        };
    }

    public static PatchEquipmentSchemaDto Convert(PatchEquipmentSchemaModel patchModel)
    {
        return new PatchEquipmentSchemaDto();
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