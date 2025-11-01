namespace Rooms.Core.Dtos.Requests.OperatorRooms;

public record PatchOperatorRoomDto
{
    public required string Name { get; set; }
    public List<int> RoomIds { get; set; } = [];
    public Dictionary<string, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}