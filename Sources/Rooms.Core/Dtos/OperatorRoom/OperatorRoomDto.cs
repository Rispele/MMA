namespace Rooms.Core.Dtos.OperatorRoom;

public class OperatorRoomDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Dictionary<int, string> Rooms { get; set; } = new();
    public Dictionary<Guid, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}