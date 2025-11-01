using Rooms.Core.Dtos.OperatorRoom;
using Rooms.Domain.Models.OperatorRoom;

namespace Rooms.Core.DtoConverters;

public static partial class OperatorRoomDtoConverter
{
    public static OperatorRoomDto Convert(OperatorRoom entity)
    {
        return new OperatorRoomDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Rooms = entity.Rooms.ToDictionary(x => x.Id, x => x.Name),
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }
}