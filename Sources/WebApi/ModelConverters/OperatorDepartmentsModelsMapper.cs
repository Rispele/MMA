using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;
using WebApi.Models.OperatorDepartments;
using WebApi.Models.Requests;
using WebApi.Models.Requests.OperatorDepartments;

namespace WebApi.ModelConverters;

[Mapper]
public static partial class OperatorDepartmentsModelsMapper
{
    [MapProperty(nameof(GetRequest<OperatorDepartmentsFilterModel>.PageSize), nameof(GetOperatorDepartmentsDto.BatchSize))]
    [MapProperty(nameof(GetRequest<OperatorDepartmentsFilterModel>.Page), nameof(GetOperatorDepartmentsDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetOperatorDepartmentsDto Convert(GetRequest<OperatorDepartmentsFilterModel> model);

    public static partial OperatorDepartmentModel Convert(OperatorDepartmentDto entity);

    public static partial PatchOperatorDepartmentDto Convert(PatchOperatorDepartmentModel patchModel);

    public static CreateOperatorDepartmentDto Convert(CreateOperatorDepartmentModel model)
    {
        return new CreateOperatorDepartmentDto
        {
            Name = model.Name,
            RoomIds = model.RoomIds,
            Operators = model.Operators,
            Contacts = model.Contacts
        };
    }

    public static PatchOperatorDepartmentModel ConvertToPatchModel(OperatorDepartmentDto entity)
    {
        return new PatchOperatorDepartmentModel
        {
            Name = entity.Name,
            RoomIds = entity.Rooms.Select(t => t.RoomId).ToArray(),
            Operators = entity.Operators.ToDictionary(),
            Contacts = entity.Contacts
        };
    }
}