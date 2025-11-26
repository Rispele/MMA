using Rooms.Core.Dtos.Files;
using Rooms.Core.Services.Spreadsheets.Abstractions;

namespace Rooms.Core.Services.Spreadsheets;

public static class SpreadsheetsExporterExtensions
{
    public static FileExportDto Export<TSpecification, TData>(
        this ISpreadsheetExporter exporter, 
        TData[] data,
        Stream outputStream,
        CancellationToken cancellationToken)
        where TSpecification : struct, ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<TData>
    {
        return exporter.Export<TSpecification, TSpecification, TData>(data, outputStream, cancellationToken);
    }
}