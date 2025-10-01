namespace Domain.Models.Room;

public class RoomAttachments(File pdfRoomScheme, File photo)
{
    public File PdfRoomScheme { get; private set; } = pdfRoomScheme;
    public File Photo { get; private set; } = photo;
}