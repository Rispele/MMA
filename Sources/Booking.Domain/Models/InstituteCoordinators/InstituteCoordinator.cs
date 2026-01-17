using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.InstituteCoordinators;

[GenerateFieldNames]
public class InstituteCoordinator
{
    private readonly int? id;
    private List<Coordinator> coordinators = null!;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private InstituteCoordinator()
    {
    }

    public InstituteCoordinator(Guid instituteId, Coordinator[] coordinators)
    {
        InstituteId = instituteId;
        this.coordinators = coordinators.ToList();
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public Guid InstituteId { get; private set; }
    public IReadOnlyList<Coordinator> Coordinators => coordinators;

    public void Update(Guid instituteId, Coordinator[] coordinatorsToSet)
    {
        InstituteId = instituteId;
        coordinators = coordinators.ToList();
    }

    # region For Tests

    public InstituteCoordinator(int id, Guid instituteId, Coordinator[] coordinators)
        : this(instituteId, coordinators)
    {
        this.id = id;
    }

    # endregion
}