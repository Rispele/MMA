using Rooms.Core.Dtos.Room;
using Rooms.Core.ExcelExporters.Writers;

namespace Rooms.Core.ExcelExporters.Exporters;

public class RoomRegistryExcelExporter : ExcelExporterBase<RoomRegistryExcelExportDto>
{
    protected override string SheetName => "Аудитории";

    protected override string FileName => "Реестр аудиторий.xlsx";

    protected override ExcelWriterBase<RoomRegistryExcelExportDto> Writer { get; } =
        new RoomRegistryExcelExportWriter();
}