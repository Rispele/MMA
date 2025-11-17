namespace Rooms.Domain.Models.OperatorDepartments;

/// <summary>
/// Операторская
/// </summary>
public class OperatorDepartment
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название операторской
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Аудитории
    /// </summary>
    public List<Room.Room> Rooms { get; } = [];

    /// <summary>
    /// Операторы
    /// </summary>
    public Dictionary<string, string> Operators { get; set; } = new();

    /// <summary>
    /// Контакты
    /// </summary>
    public string Contacts { get; set; } = null!;

    public void Update(
        string name,
        Dictionary<string, string> operators,
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