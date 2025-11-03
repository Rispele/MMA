namespace Rooms.Core.Dtos.Requests.OperatorRooms;

public record CreateOperatorRoomDto
{
    public required string Name { get; set; }
    public List<int> RoomIds { get; set; } = [];
    public Dictionary<Guid, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}