namespace Commons.ExternalClients.Booking.Models;

public record BookRoomRequest(
    DateOnly Date,
    string Auditory,
    TimeOnly BeginTime,
    TimeOnly EndTime,
    string Discipline,
    string Group,
    string LoadType,
    string? TeacherPkey);

internal record BookRoomInnerRequest(
    string Date,
    string Auditory,
    string BeginTime,
    string EndTime,
    string Discipline,
    string Group,
    string LoadType,
    string? TeacherPkey);