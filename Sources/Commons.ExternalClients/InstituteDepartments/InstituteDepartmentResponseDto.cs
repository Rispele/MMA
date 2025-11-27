using JetBrains.Annotations;

namespace Commons.ExternalClients.InstituteDepartments;

public class InstituteDepartmentResponseDto
{
    public string Id { get; [UsedImplicitly] private set; } = null!;
    public string InstituteName { get; [UsedImplicitly] private set; } = null!;
}