using Rooms.Core.Dtos.OperatorRoom;
using Rooms.Domain.Models.OperatorRoom;

namespace Rooms.Core.DtoConverters;

public static partial class OperatorRoomDtoConverter
{
    public static OperatorRoom Convert(OperatorRoomDto entity)
    {
        return new OperatorRoom
        {
            Id = entity.Id,
            Name = entity.Name,
            Rooms = [],
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }
}