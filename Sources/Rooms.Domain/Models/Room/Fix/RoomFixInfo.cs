// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace Rooms.Domain.Models.Room.Fix;

public class RoomFixInfo
{
    // (d.smirnov): крч оно как-то говённо иногда работает, решил пока что вырубить
    // ReSharper disable once ConvertToPrimaryConstructor
    public RoomFixInfo(RoomStatus status, DateTime? fixDeadline, string? comment)
    {
        Status = status;
        FixDeadline = fixDeadline;
        Comment = comment;
    }

    public RoomStatus Status { get; private set; }
    public DateTime? FixDeadline { get; private set; }
    public string? Comment { get; private set; }
}