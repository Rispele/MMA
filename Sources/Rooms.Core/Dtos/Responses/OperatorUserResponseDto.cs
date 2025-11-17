using JetBrains.Annotations;

namespace Rooms.Core.Dtos.Responses;

public class OperatorUserResponseDto
{
    public string Id { get; set; } = null!; // required
    [UsedImplicitly] public Guid Guid { get; [UsedImplicitly] private set; }
    public string FullName { get; set; } = null!;
    [UsedImplicitly] public string Category { get; set; } = null!;
    [UsedImplicitly] public string Type { get; set; } = null!;
    [UsedImplicitly] public string DivisionTitle { get; set; } = null!;
    [UsedImplicitly] public string Post { get; set; } = null!;
    [UsedImplicitly] public string? Qualification { get; set; }
    [UsedImplicitly] public string? InstituteTitle { get; set; }
    [UsedImplicitly] public string? GroupTitle { get; set; }
    [UsedImplicitly] public int? RoleCount { get; set; }
    [UsedImplicitly] public int? GroupCount { get; set; }
}