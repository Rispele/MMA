using WebApi.Models.OperatorRoom;

namespace WebApi.Models.Responses;

public class OperatorRoomsResponseModel
{
    public OperatorRoomModel[] OperatorRooms { get; init; } = [];
    public int Count { get; init; }
}