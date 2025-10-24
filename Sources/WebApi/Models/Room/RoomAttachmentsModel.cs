using WebApi.Models.Files;

namespace WebApi.Models.Room;

public record RoomAttachmentsModel(FileDescriptorModel? PdfRoomScheme, FileDescriptorModel? Photo);