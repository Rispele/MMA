using Commons.Optional;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.OperatorDepartments;
using WebApi.Models.Requests.Filtering;
using WebApi.Models.Requests.OperatorDepartments;

namespace WebApi.ModelConverters;

public static partial class OperatorDepartmentsModelsConverter
{
    public static GetOperatorDepartmentsDto Convert(GetOperatorDepartmentsModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new GetOperatorDepartmentsDto(
            Math.Max(val1: 0, model.Page - 1),
            model.PageSize,
            model.AfterOperatorDepartmentId,
            model.Filter
                .AsOptional()
                .Map(filter => new OperatorDepartmentsFilterDto
                {
                    Name = MapFilterParameter(filter.Name, v => v),
                    RoomName = MapFilterParameter(filter.RoomName, v => v),
                    Operator = MapFilterParameter(filter.Operator, v => v),
                    OperatorEmail = MapFilterParameter(filter.OperatorEmail, v => v),
                    Contacts = MapFilterParameter(filter.Contacts, v => v),
                }));
    }

    public static CreateOperatorDepartmentDto Convert(CreateOperatorDepartmentModel model)
    {
        return new CreateOperatorDepartmentDto
        {
            Name = model.Name,
            RoomIds = model.RoomIds,
            Operators = model.Operators,
            Contacts = model.Contacts,
        };
    }

    public static PatchOperatorDepartmentDto Convert(PatchOperatorDepartmentModel patchModel)
    {
        return null!;
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