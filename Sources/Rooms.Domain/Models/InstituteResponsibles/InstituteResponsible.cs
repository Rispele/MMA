using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Rooms.Domain.Models.InstituteResponsibles;

[GenerateFieldNames]
public class InstituteResponsible
{
    private readonly int? id;
    private Dictionary<string, string> responsible = null!;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private InstituteResponsible()
    {
    }

    public InstituteResponsible(
        string institute,
        Dictionary<string, string> responsible)
    {
        Institute = institute;
        this.responsible = responsible;
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public string Institute { get; private set; } = null!;
    public IReadOnlyDictionary<string, string> Responsible => responsible;

    public void Update(
        string institute,
        Dictionary<string, string> responsibleToSet)
    {
        Institute = institute;
        responsible = responsibleToSet;
    }

    # region For Tests

    public InstituteResponsible(
        int id,
        string institute,
        Dictionary<string, string> responsible) : this(institute, responsible)
    {
        this.id = id;
    }

    # endregion
}