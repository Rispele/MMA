using Rooms.Core.Dtos.OperatorDepartments;
using WebApi.Models.OperatorDepartments;
using WebApi.Models.Requests.OperatorDepartments;

namespace WebApi.ModelConverters;

public static partial class OperatorDepartmentsModelsConverter
{
    public static OperatorDepartmentModel Convert(OperatorDepartmentDto entity)
    {
        return new OperatorDepartmentModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Rooms = entity.Rooms,
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }

    public static PatchOperatorDepartmentModel ConvertToPatchModel(OperatorDepartmentDto entity)
    {
        return new PatchOperatorDepartmentModel
        {
            Name = entity.Name,
            RoomIds = entity.Rooms.Keys,
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }
}