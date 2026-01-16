namespace WebApi.Core.Models.InstituteCoordinator;

public record InstituteDepartmentResponseModel(
    string Id,
    string InstituteName,
    string? ParentId);