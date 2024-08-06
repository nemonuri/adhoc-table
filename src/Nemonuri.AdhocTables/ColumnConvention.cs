namespace Nemonuri.AdhocTables;

public record ColumnConvention
{
    public ColumnConvention(string columnName, CellDataConvention cellDataConvention)
    {
        Guard.IsNotNull(columnName);
        Guard.IsNotNull(cellDataConvention);
        ColumnName = columnName;
        CellDataConvention = cellDataConvention;
    }

    public string ColumnName {get;}
    public CellDataConvention CellDataConvention {get;}
    public ColumnConventionBuilder ToBuilder => new() {
        ColumnName = ColumnName,
        CellDataConvention = CellDataConvention
    };
}

public class ColumnConventionBuilder()
{
    public string? ColumnName {get;set;}
    public CellDataConvention? CellDataConvention {get;set;}

    [MemberNotNullWhen(true, nameof(ColumnName), nameof(CellDataConvention))]
    public bool IsValid => ColumnName != null && CellDataConvention != null;

    public ColumnConvention? Build() => IsValid ? new ColumnConvention(ColumnName, CellDataConvention) : null;
}