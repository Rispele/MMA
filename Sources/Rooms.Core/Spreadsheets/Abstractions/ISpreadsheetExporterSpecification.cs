namespace Rooms.Core.Spreadsheets.Abstractions;

public interface ISpreadsheetExporterSpecification
{
    public string SheetName { get; }
    public string FileName { get; }
}