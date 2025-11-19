namespace Rooms.Core.Spreadsheets.Abstractions;

public interface ISpreadsheetWriterSpecification<in TData>
{
    public IReadOnlyList<string> ColumnNames { get; }
    public IEnumerable<ColumnCellData> GetValuesToWrite(TData data);
}