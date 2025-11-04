using Rooms.Core.Dtos.Equipment;
using Rooms.Core.ExcelExporters.Writers;

namespace Rooms.Core.ExcelExporters.Exporters;

public class EquipmentRegistryExcelExporter : ExcelExporterBase<EquipmentRegistryExcelExportDto>
{
    protected override string SheetName => "Оборудование";

    protected override string FileName => "Реестр оборудования.xlsx";

    protected override ExcelWriterBase<EquipmentRegistryExcelExportDto> Writer { get; } =
        new EquipmentRegistryExcelExportWriter();
}