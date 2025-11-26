using Rooms.Core.Services.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Services.Spreadsheets.Specifications;

namespace Rooms.Core.Services.Spreadsheets.Abstractions;

public interface ISpreadsheetWriterSpecification<TData>
{
    public IReadOnlyList<ColumnSpecification<TData>> ColumnSpecifications { get; }

    public IEnumerable<ISpreadsheetValueType> GetValuesToWrite(TData data)
    {
        return ColumnSpecifications.Select(specification => specification.ValueExtractor(data));
    }
}