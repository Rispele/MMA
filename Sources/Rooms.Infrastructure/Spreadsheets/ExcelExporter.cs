using NPOI.XSSF.UserModel;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Spreadsheets.Abstractions;

namespace Rooms.Infrastructure.Spreadsheets;

public class ExcelExporter : ISpreadsheetExporter
{
    private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public FileExportDto Export<TExporterSpecification, TWriterSpecification, TData>(TData[] data, CancellationToken cancellationToken)
        where TExporterSpecification : struct, ISpreadsheetExporterSpecification
        where TWriterSpecification : struct, ISpreadsheetWriterSpecification<TData>
    {
        var workbook = new XSSFWorkbook();

        var exporterSpecification = new TExporterSpecification();
        var worksheet = workbook.CreateSheet(exporterSpecification.SheetName);

        ExcelWriter.Write<TWriterSpecification, TData>(worksheet, data);

        return new FileExportDto
        {
            FileName = exporterSpecification.FileName,
            Content = ToMemoryStreamAsync(workbook),
            ContentType = ContentType,
        };
    }

    private static MemoryStream ToMemoryStreamAsync(XSSFWorkbook workbook)
    {
        var memoryStream = new MemoryStream();
        
        workbook.Write(memoryStream, leaveOpen: true);
        if (memoryStream.CanSeek)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
        }
        
        return memoryStream;
    }
}