using Rooms.Core.Implementations.Dtos.Requests.EquipmentCreating;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentPatching;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentQuerying;
using Rooms.Core.Implementations.Dtos.Requests.Filtering;
using WebApi.Models.Requests;
using WebApi.Models.Requests.Filtering;

namespace WebApi.Services.Implementations;

public static partial class EquipmentsModelsConverter
{
    public static GetEquipmentsRequestDto Convert(EquipmentsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new GetEquipmentsRequestDto
        {
        };
    }

    public static CreateEquipmentRequest Convert(CreateEquipmentModel model)
    {
        return new CreateEquipmentRequest
        {
        };
    }

    public static PatchEquipmentRequest Convert(PatchEquipmentModel patchModel)
    {
        return new PatchEquipmentRequest
        {
        };
    }

    private static FilterParameterDto<TOut>? MapFilterParameter<TIn, TOut>(FilterParameterModel<TIn>? src, Func<TIn, TOut> map)
    {
        if (src == null || src.Value == null) return null;
        return new FilterParameterDto<TOut>(map(src.Value), Convert(src.SortDirection));
    }

    private static FilterMultiParameterDto<TOut>? MapFilterMultiParameter<TIn, TOut>(FilterMultiParameterModel<TIn>? src, Func<TIn, TOut> map)
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
}