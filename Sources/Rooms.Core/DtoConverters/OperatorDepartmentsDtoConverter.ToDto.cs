using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.DtoConverters;

public static partial class OperatorDepartmentsDtoConverter
{
    public static OperatorDepartmentDto Convert(OperatorDepartment entity)
    {
        return new OperatorDepartmentDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Rooms = entity.Rooms.ToDictionary(x => x.Id, x => x.Name),
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }
}