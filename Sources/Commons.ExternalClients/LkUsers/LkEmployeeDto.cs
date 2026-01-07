namespace Commons.ExternalClients.LkUsers;

public record LkEmployeeDto(
    string PersonId,
    string UserId,
    string FullName,
    string InstituteId,
    string Institute,
    string Category,
    string Post,
    string TeacherKey);