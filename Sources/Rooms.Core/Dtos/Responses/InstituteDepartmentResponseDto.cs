using JetBrains.Annotations;

namespace Rooms.Core.Dtos.Responses;

public class InstituteDepartmentResponseDto
{
    public string Id { get; [UsedImplicitly] private set; } = null!;
    public string InstituteName { get; [UsedImplicitly] private set; } = null!;
}