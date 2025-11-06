using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.DtoConverters;

public static partial class OperatorDepartmentsDtoConverter
{
    public static OperatorDepartment Convert(OperatorDepartmentDto entity)
    {
        return new OperatorDepartment
        {
            Id = entity.Id,
            Name = entity.Name,
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }
}