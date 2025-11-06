namespace Rooms.Domain.Models.OperatorDepartments;

public class OperatorDepartment
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Room.Room> Rooms { get; } = [];
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

    public void AddRoom(Room.Room room)
    {
        if (Rooms.Any(r => r.Id == room.Id))
        {
            return;
        }
        
        Rooms.Add(room);
    }
    
    public void RemoveRoom(int roomId)
    {
        Rooms.RemoveAll(t => t.Id == roomId);
    }
}