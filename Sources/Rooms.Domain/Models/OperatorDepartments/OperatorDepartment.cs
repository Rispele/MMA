using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Rooms.Domain.Models.OperatorDepartments;

[GenerateFieldNames]
internal class OperatorDepartment
{
    private readonly int? id;

    private Dictionary<string, string> operators = null!;
    private readonly List<Rooms.Room> rooms = null!;

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public string Name { get; private set; } = null!;
    public string Contacts { get; private set; } = null!;

    public IEnumerable<Rooms.Room> Rooms => rooms;
    public IReadOnlyDictionary<string, string> Operators => operators;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private OperatorDepartment()
    {
    }

    public OperatorDepartment(string name, string contacts, Dictionary<string, string> operators)
    {
        Name = name;
        Contacts = contacts;
        this.operators = operators;
    }

    public void Update(
        string name,
        Dictionary<string, string> operatorsToSet,
        string contacts)
    {
        Name = name;
        Contacts = contacts;

        operators = operatorsToSet;
    }

    public void AddRoom(Rooms.Room room)
    {
        if (Rooms.Any(r => r.Id == room.Id))
        {
            return;
        }

        rooms.Add(room);
    }

    public void RemoveRoom(int roomId)
    {
        rooms.RemoveAll(t => t.Id == roomId);
    }

    #region For Tests

    /// <summary>
    /// Use only for tests, ORM handles id initialization
    /// </summary>
    public OperatorDepartment(
        int id,
        string name,
        string contacts,
        Dictionary<string, string> operators) : this(name, contacts, operators)
    {
        this.id = id;
    }

    #endregion
}