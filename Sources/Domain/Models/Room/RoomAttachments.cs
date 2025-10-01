using JetBrains.Annotations;

namespace Domain.Models.Room;

public class RoomAttachments
{
    public File? PdfRoomScheme { get; private set; }
    public File? Photo { get; private set; }

    [UsedImplicitly]
    protected RoomAttachments()
    {
    }

    public RoomAttachments(File? pdfRoomScheme, File? photo)
    {
        PdfRoomScheme = pdfRoomScheme;
        Photo = photo;
    }
}