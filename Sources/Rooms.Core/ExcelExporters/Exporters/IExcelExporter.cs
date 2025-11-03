using Rooms.Core.Dtos;

namespace Rooms.Core.ExcelExporters.Exporters;

public interface IExcelExporter<in TExportValue>
{
    string SheetName { get; }

    public FileExportDto Export(TExportValue[] data, CancellationToken cancellationToken);
}