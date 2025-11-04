using Rooms.Core.Dtos.Requests.Filtering;
using WebApi.Models.Requests.Filtering;

namespace WebApi.ModelConverters;

public static class FilterModelMapper
{
    public static FilterParameterDto<TOut>? MapFilterParameter<TIn, TOut>(
        FilterParameterModel<TIn>? src,
        Func<TIn, TOut> map)
    {
        if (src == null || src.Value == null)
        {
            return null;
        }

        return new FilterParameterDto<TOut>(map(src.Value), Convert(src.SortDirection));
    }

    public static FilterMultiParameterDto<TOut>? MapFilterMultiParameter<TIn, TOut>(
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