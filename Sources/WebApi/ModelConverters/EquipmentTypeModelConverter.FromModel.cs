using Commons.Optional;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Dtos.Requests.Filtering;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Requests.Filtering;

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
                    Name = MapFilterParameter(filter.Name, map: v => v)
                }));
    }

    public static CreateEquipmentTypeDto Convert(CreateEquipmentTypeModel model)
    {
        return new CreateEquipmentTypeDto
        {
            Name = model.Name,
            Parameters = model.Parameters.Select(x => new EquipmentParameterDescriptorDto
            {
                Name = x.Name,
                Required = x.Required,
            })
        };
    }

    public static PatchEquipmentTypeDto Convert(PatchEquipmentTypeModel patchModel)
    {
        return new PatchEquipmentTypeDto
        {
            Name = patchModel.Name,
            Parameters = patchModel.Parameters.Select(x => new EquipmentParameterDescriptorDto
            {
                Name = x.Name,
                Required = x.Required,
            })
        };
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