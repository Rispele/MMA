using JetBrains.Annotations;
using Rooms.Domain.Models.FileModels;

namespace Rooms.Domain.Models.RoomModels;

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