using NPOI.XSSF.UserModel;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Services.Spreadsheets.Abstractions;

namespace Rooms.Infrastructure.Services.Spreadsheets;

public class ExcelExporter : ISpreadsheetExporter
{
    private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public FileExportDto Export<TExporterSpecification, TWriterSpecification, TData>(
        TData[] data,
        Stream outputStream,
        CancellationToken cancellationToken)
        where TExporterSpecification : struct, ISpreadsheetExporterSpecification
        where TWriterSpecification : struct, ISpreadsheetWriterSpecification<TData>
    {
        var exporterSpecification = new TExporterSpecification();
        var writerSpecification = new TWriterSpecification();

        return Export(exporterSpecification, writerSpecification, data, outputStream);
    }

    public FileExportDto Export<TData>(
        ISpreadsheetExporterSpecification exporterSpecification,
        ISpreadsheetWriterSpecification<TData> writerSpecification,
        TData[] data,
        Stream outputStream)
    {
        var workbook = new XSSFWorkbook();

        var worksheet = workbook.CreateSheet(exporterSpecification.SheetName);

        ExcelWriter.Write(writerSpecification, worksheet, data);

        return new FileExportDto
        {
            FileName = exporterSpecification.FileName,
            ContentType = ContentType,
            Flush = () => workbook.Write(outputStream, leaveOpen: true)
        };
    }
}