using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Rooms.Domain.Models.Equipments;

[GenerateFieldNames]
public class EquipmentSchema
{
    private readonly int? id;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private EquipmentSchema()
    {
    }

    public EquipmentSchema(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues)
    {
        Name = name;
        Type = equipmentType;
        ParameterValues = parameterValues;
    }

    public int Id => id ?? throw new InvalidOperationException("Equipment id not initialized yet");
    public string Name { get; private set; }
    public EquipmentType Type { get; private set; } = null!;
    public Dictionary<string, string> ParameterValues { get; private set; }

    public void Update(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues)
    {
        Name = name;
        Type = equipmentType;
        ParameterValues = parameterValues;
    }

    #region For Tests

    /// <summary>
    /// Use only for tests, ORM handles id initialization
    /// </summary>
    internal EquipmentSchema(
        int id,
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues) : this(name, equipmentType, parameterValues)
    {
        this.id = id;
    }

    #endregion
}