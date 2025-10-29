using Commons;
using Rooms.Core.Dtos.Equipment;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentTypes;

namespace WebApi.ModelConverters;

public static partial class EquipmentTypesModelsConverter
{
    public static EquipmentTypeModel Convert(EquipmentTypeDto type)
    {
        return new EquipmentTypeModel
        {
            Id = type.Id,
            Name = type.Name,
            Parameters = type.Parameters.Select(x => x.Map(Convert))
        };
    }

    public static EquipmentParameterDescriptorModel Convert(EquipmentParameterDescriptorDto dto)
    {
        return new EquipmentParameterDescriptorModel
        {
            Name = dto.Name,
            Required = dto.Required
        };
    }

    public static PatchEquipmentTypeModel ConvertToPatchModel(EquipmentTypeDto dto)
    {
        return new PatchEquipmentTypeModel
        {
            Name = dto.Name,
            Parameters = dto.Parameters.Select(x => x.Map(Convert))
        };
    }
}