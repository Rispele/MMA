using Rooms.Domain.Models.File;

namespace Rooms.Domain.Models.Rooms;

public class RoomAttachments
{
    public FileDescriptor? PdfRoomScheme { get; set; }
    public FileDescriptor? Photo { get; set; }
}