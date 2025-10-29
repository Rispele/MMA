using Mapster;
using Rooms.Core.Dtos.Room;
using WebApi.Models.Room;

namespace WebApi.ModelConverters;

[Mapper]
public interface IRoomConverter
{
    public RoomModel MapTo(RoomDto room);
}