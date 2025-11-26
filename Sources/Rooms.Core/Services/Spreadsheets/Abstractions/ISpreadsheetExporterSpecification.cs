namespace Rooms.Core.Services.Spreadsheets.Abstractions;

public interface ISpreadsheetExporterSpecification
{
    public string SheetName { get; }
    public string FileName { get; }
}