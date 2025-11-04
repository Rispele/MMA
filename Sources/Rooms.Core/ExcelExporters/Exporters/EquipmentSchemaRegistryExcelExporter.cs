using Rooms.Core.Dtos.Equipment;
using Rooms.Core.ExcelExporters.Writers;

namespace Rooms.Core.ExcelExporters.Exporters;

public class EquipmentSchemaRegistryExcelExporter : ExcelExporterBase<EquipmentSchemaRegistryExcelExportDto>
{
    protected override string SheetName => "Модели оборудования";

    protected override string FileName => "Модели оборудования.xlsx";

    protected override ExcelWriterBase<EquipmentSchemaRegistryExcelExportDto> Writer { get; } =
        new EquipmentSchemaRegistryExcelExportWriter();
}