using NPOI.XSSF.UserModel;
using Rooms.Core.Dtos;
using Rooms.Core.ExcelExporters.Writers;

namespace Rooms.Core.ExcelExporters.Exporters;

public abstract class ExcelExporterBase<TExportValue>
{
    protected abstract string SheetName { get; }

    protected abstract string FileName { get; }

    protected abstract ExcelWriterBase<TExportValue> Writer { get; }

    const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    internal FileExportDto Export(TExportValue[] data, CancellationToken cancellationToken = default)
    {
        var workbook = new XSSFWorkbook();
        var worksheet = workbook.CreateSheet(SheetName);
        Writer.Write(worksheet, data);

        var memoryStream = new MemoryStream();
        workbook.Write(memoryStream, leaveOpen: true);
        if (memoryStream.CanSeek)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
        }

        return new FileExportDto
        {
            FileName = FileName,
            Content = memoryStream,
            ContentType = ContentType,
        };
    }
}