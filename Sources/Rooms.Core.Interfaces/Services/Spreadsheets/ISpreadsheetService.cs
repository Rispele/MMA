using Rooms.Core.Interfaces.Dtos.Files;

namespace Rooms.Core.Interfaces.Services.Spreadsheets;

public interface ISpreadsheetService
{
    public Task<FileExportDto> ExportRoomRegistry(Stream outputStream, CancellationToken cancellationToken);
    public Task<FileExportDto> ExportEquipmentRegistry(Stream outputStream, CancellationToken cancellationToken);
    public Task<FileExportDto> ExportEquipmentSchemaRegistry(Stream outputStream, CancellationToken cancellationToken);
    public Task<FileExportDto> ExportEquipmentTypeRegistry(Stream outputStream, CancellationToken cancellationToken);
}