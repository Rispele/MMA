using NPOI.XSSF.UserModel;
using Rooms.Core.Dtos;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.ExcelExporters.Writers;

namespace Rooms.Core.ExcelExporters.Exporters;

public class EquipmentSchemaRegistryExcelExporter : IExcelExporter<EquipmentSchemaRegistryExcelExportDto>
{
    public string SheetName => "Модели оборудования";

    public FileExportDto Export(EquipmentSchemaRegistryExcelExportDto[] exportDtos, CancellationToken cancellationToken = default)
    {
        using var workbook = new XSSFWorkbook();
        var writer = new EquipmentSchemaRegistryExcelExportWriter();
        var worksheet = workbook.CreateSheet(SheetName);
        writer.Write(worksheet, exportDtos);

        using var memoryStream = new MemoryStream();
        workbook.Write(memoryStream);

        return new FileExportDto
        {
            FileName = "Модели оборудования.xlsx",
            Content = memoryStream,
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        };
    }
}