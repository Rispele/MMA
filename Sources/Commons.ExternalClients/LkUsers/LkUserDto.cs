namespace Commons.ExternalClients.LkUsers;

public record LkUserDto(
    string PersonId,
    string UserId,
    string FullName,
    string FirstName,
    string LastName,
    string? MiddleName,
    string Email);