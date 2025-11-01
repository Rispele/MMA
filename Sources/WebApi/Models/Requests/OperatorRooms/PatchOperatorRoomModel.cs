namespace WebApi.Models.Requests.OperatorRooms;

public record PatchOperatorRoomModel
{
    public required string Name { get; set; }
    public IEnumerable<int> RoomIds { get; set; } = [];
    public Dictionary<string, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;
}