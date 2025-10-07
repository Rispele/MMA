using JetBrains.Annotations;

namespace Rooms.Domain.Models.Room;

public class RoomAttachments
{
    public FileDescriptor? PdfRoomScheme { get; private set; }
    public FileDescriptor? Photo { get; private set; }

    [UsedImplicitly]
    protected RoomAttachments()
    {
    }

    public RoomAttachments(FileDescriptor? pdfRoomScheme, FileDescriptor? photo)
    {
        PdfRoomScheme = pdfRoomScheme;
        Photo = photo;
    }
}