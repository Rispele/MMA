using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Rooms.Domain.Models.InstituteCoordinators;

[GenerateFieldNames]
public class InstituteCoordinator
{
    private readonly int? id;
    private List<Coordinator> coordinators = null!;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private InstituteCoordinator()
    {
    }

    public InstituteCoordinator(string institute, Coordinator[] coordinators)
    {
        Institute = institute;
        this.coordinators = coordinators.ToList();
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public string Institute { get; private set; } = null!;
    public IReadOnlyList<Coordinator> Coordinators => coordinators;

    public void Update(string institute, Coordinator[] coordinatorsToSet)
    {
        Institute = institute;
        coordinators = coordinators.ToList();
    }

    # region For Tests

    public InstituteCoordinator(int id, string institute, Coordinator[] coordinators)
        : this(institute, coordinators)
    {
        this.id = id;
    }

    # endregion
}