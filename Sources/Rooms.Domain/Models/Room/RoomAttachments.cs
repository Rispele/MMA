using Rooms.Domain.Models.File;

namespace Rooms.Domain.Models.Room;

public class RoomAttachments
{
    public FileDescriptor? PdfRoomScheme { get; set; }
    public FileDescriptor? Photo { get; set; }
}