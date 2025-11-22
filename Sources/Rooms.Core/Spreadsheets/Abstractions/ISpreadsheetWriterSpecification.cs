using Rooms.Core.Spreadsheets.ExcelValueTypes;
using Rooms.Core.Spreadsheets.Specifications;

namespace Rooms.Core.Spreadsheets.Abstractions;

public interface ISpreadsheetWriterSpecification<TData>
{
    public IReadOnlyList<ColumnSpecification<TData>> ColumnSpecifications { get; }

    public IEnumerable<ISpreadsheetValueType> GetValuesToWrite(TData data)
    {
        return ColumnSpecifications.Select(specification => specification.ValueExtractor(data));
    }
}