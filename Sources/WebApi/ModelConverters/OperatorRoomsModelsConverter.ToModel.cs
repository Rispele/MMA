using Rooms.Core.Dtos.OperatorRoom;
using WebApi.Models.OperatorRoom;
using WebApi.Models.Requests.OperatorRooms;

namespace WebApi.ModelConverters;

public static partial class OperatorRoomsModelsConverter
{
    public static OperatorRoomModel Convert(OperatorRoomDto entity)
    {
        return new OperatorRoomModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Rooms = entity.Rooms,
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }

    public static PatchOperatorRoomModel ConvertToPatchModel(OperatorRoomDto entity)
    {
        return new PatchOperatorRoomModel
        {
            Name = entity.Name,
            RoomIds = entity.Rooms.Keys,
            Operators = entity.Operators,
            Contacts = entity.Contacts,
        };
    }
}