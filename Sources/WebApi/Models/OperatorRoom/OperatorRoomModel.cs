namespace WebApi.Models.OperatorRoom;

public class OperatorRoomModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Dictionary<int, string> Rooms { get; set; } = new();
    public Dictionary<string, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}