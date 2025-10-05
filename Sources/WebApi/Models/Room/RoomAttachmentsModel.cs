using WebApi.Models.Files;

namespace WebApi.Models.Room;

public record RoomAttachmentsModel(FileMetadataModel? PdfRoomScheme, FileMetadataModel? Photo);
