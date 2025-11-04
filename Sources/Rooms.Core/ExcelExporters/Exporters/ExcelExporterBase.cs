using NPOI.XSSF.UserModel;
using Rooms.Core.Dtos;
using Rooms.Core.ExcelExporters.Writers;

namespace Rooms.Core.ExcelExporters.Exporters;

public abstract class ExcelExporterBase<TExportValue>
{
    protected virtual string SheetName { get; }

    protected virtual string FileName { get; }

    protected virtual ExcelWriterBase<TExportValue> Writer { get; }

    protected string ContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public virtual FileExportDto Export(TExportValue[] data, CancellationToken cancellationToken = default)
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