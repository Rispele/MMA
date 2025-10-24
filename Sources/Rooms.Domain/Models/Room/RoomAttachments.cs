using JetBrains.Annotations;
using Rooms.Domain.Models.File;

namespace Rooms.Domain.Models.Room;

public class RoomAttachments
{
    [UsedImplicitly]
    protected RoomAttachments()
    {
    }

    public RoomAttachments(FileDescriptor? pdfRoomScheme, FileDescriptor? photo)
    {
        PdfRoomScheme = pdfRoomScheme;
        Photo = photo;
    }

    public FileDescriptor? PdfRoomScheme { get; private set; }
    public FileDescriptor? Photo { get; private set; }
}