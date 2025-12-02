using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Rooms.Domain.Models.Equipments;

[GenerateFieldNames]
internal class EquipmentSchema
{
    private readonly int? id;
    private Dictionary<string, string> parameterValues = null!;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private EquipmentSchema()
    {
    }

    public EquipmentSchema(
        string name,
        int equipmentTypeId,
        Dictionary<string, string> parameterValues)
    {
        Name = name;
        EquipmentTypeId = equipmentTypeId;
        this.parameterValues = parameterValues;
    }

    public int Id => id ?? throw new InvalidOperationException("Equipment id not initialized yet");
    public IReadOnlyDictionary<string, string> ParameterValues => parameterValues;
    public string Name { get; private set; } = null!;
    public int EquipmentTypeId { get; private set; }

    public void Update(
        string name,
        int equipmentTypeId,
        Dictionary<string, string> parameterValuesToSet)
    {
        Name = name;
        EquipmentTypeId = equipmentTypeId;
        parameterValues = parameterValuesToSet;
    }

    #region For Tests

    /// <summary>
    /// Use only for tests, ORM handles id initialization
    /// </summary>
    internal EquipmentSchema(
        int id,
        string name,
        int equipmentTypeId,
        Dictionary<string, string> parameterValues) : this(name, equipmentTypeId, parameterValues)
    {
        this.id = id;
    }

    #endregion
}