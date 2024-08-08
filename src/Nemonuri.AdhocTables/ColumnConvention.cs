namespace Nemonuri.AdhocTables;

public record ColumnConvention
{
    public ColumnConvention(string columnName, CellConvention cellConvention)
    {
        Guard.IsNotNull(columnName);
        Guard.IsNotNull(cellConvention);
        ColumnName = columnName;
        CellConvention = cellConvention;
    }

    public string ColumnName {get;}
    public CellConvention CellConvention {get;}
    public ColumnConventionBuilder ToBuilder => new() {
        ColumnName = ColumnName,
        CellConvention = CellConvention
    };
}

public class ColumnConventionBuilder()
{
    public string? ColumnName {get;set;}
    public CellConvention? CellConvention {get;set;}

    [MemberNotNullWhen(true, nameof(ColumnName), nameof(CellConvention))]
    public bool IsValidToBuild => ColumnName != null && CellConvention != null;

    public ColumnConvention? Build() => IsValidToBuild ? new ColumnConvention(ColumnName, CellConvention) : null;
}