namespace Domain.Models.Room;

public class RoomAttachments
{
    public File PdfRoomScheme { get; private set; }
    public File Photo { get; private set; }
    
    public RoomAttachments(File pdfRoomScheme, File photo)
    {
        PdfRoomScheme = pdfRoomScheme;
        Photo = photo;
    }
}