namespace Commons.ExternalClients.InstituteDepartments;

public record InstituteDepartmentResponseDto(
    string Id,
    string InstituteName,
    string? ParentId);