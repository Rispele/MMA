using Rooms.Core.Dtos.Equipment;
using Rooms.Core.ExcelExporters.Writers;

namespace Rooms.Core.ExcelExporters.Exporters;

public class EquipmentTypeRegistryExcelExporter : ExcelExporterBase<EquipmentTypeRegistryExcelExportDto>
{
    protected override string SheetName => "Типы оборудования";

    protected override string FileName => "Типы оборудования.xlsx";

    protected override ExcelWriterBase<EquipmentTypeRegistryExcelExportDto> Writer { get; } =
        new EquipmentTypeRegistryExcelExportWriter();
}