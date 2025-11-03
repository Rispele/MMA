namespace Rooms.Domain.Models.OperatorRoom;

public class OperatorRoom
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Room.Room> Rooms { get; set; } = [];
    public Dictionary<Guid, string> Operators { get; set; } = new();
    public string Contacts { get; set; } = null!;

    public void Update(
        string name,
        Dictionary<Guid, string> operators,
        string contacts)
    {
        Name = name;
        Operators = operators;
        Contacts = contacts;
    }
}