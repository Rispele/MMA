using Rooms.Core.Dtos.Files;

namespace Rooms.Core.Spreadsheets.Abstractions;

public interface ISpreadsheetExporter
{
    public FileExportDto Export<TExporterSpecification, TWriterSpecification, TData>(
        TData[] data,
        Stream outputStream,
        CancellationToken cancellationToken)
        where TExporterSpecification : struct, ISpreadsheetExporterSpecification
        where TWriterSpecification : struct, ISpreadsheetWriterSpecification<TData>;
}