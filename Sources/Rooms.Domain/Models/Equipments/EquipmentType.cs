using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Rooms.Domain.Models.Equipments;

[GenerateFieldNames]
public class EquipmentType
{
    private readonly int? id;
    private List<EquipmentParameterDescriptor> parameters = null!;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private EquipmentType()
    {
    }

    public EquipmentType(
        string name,
        List<EquipmentParameterDescriptor> parameters)
    {
        Name = name;
        this.parameters = parameters;
    }

    public int Id => id ?? throw new InvalidOperationException("Equipment Type id is not initialized yet");
    public IReadOnlyList<EquipmentParameterDescriptor> Parameters => parameters;
    public string Name { get; private set; } = null!;

    public void Update(
        string name,
        List<EquipmentParameterDescriptor> parametersToSet)
    {
        Name = name;
        parameters = parametersToSet;
    }

    #region For Tests

    /// <summary>
    /// Use only for tests, ORM handles id initialization
    /// </summary>
    internal EquipmentType(
        int id,
        string name,
        List<EquipmentParameterDescriptor> parameters) : this(name, parameters)
    {
        this.id = id;
    }

    #endregion
}