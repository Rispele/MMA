using Rooms.Core.Dtos.Files;
using Rooms.Core.Spreadsheets.Abstractions;

namespace Rooms.Core.Spreadsheets;

public static class SpreadsheetsExporterExtensions
{
    public static FileExportDto Export<TSpecification, TData>(this ISpreadsheetExporter exporter, TData[] data, CancellationToken cancellationToken)
        where TSpecification : struct, ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<TData>
    {
        return exporter.Export<TSpecification, TSpecification, TData>(data, cancellationToken);
    }
}