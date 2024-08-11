namespace Nemonuri.AdhocTables;

public record ColumnConvention
{
    public ColumnConvention(
        string? columnName = null, 
        CellConvention? cellConvention = null,
        bool isUnique = false,
        bool isPrimaryKey = false,
        CultureInfo? cultureInfo = null,
        IReferenceConvention? referenceConvention = null)
    {
        ColumnName = columnName ?? CreateDefaultColumnName();
        CellConvention = cellConvention ?? new CellConvention();
        IsUnique = isUnique;
        IsPrimaryKey = isPrimaryKey;
        CultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
        ReferenceConvention = referenceConvention;
    }

    public string ColumnName {get;}
    public CellConvention CellConvention {get;}
    public bool IsUnique {get;}
    public bool IsPrimaryKey {get;}
    public CultureInfo CultureInfo {get;}
    public IReferenceConvention? ReferenceConvention {get;}

    public ColumnConventionBuilder ToBuilder => new() {
        ColumnName = ColumnName,
        CellConvention = CellConvention,
        IsUnique = IsUnique,
        IsPrimaryKey = IsPrimaryKey,
        CultureInfo = CultureInfo,
        ReferenceConvention = ReferenceConvention
    };


    private const string DefaultColumnNamePrefix = "Column";
    private static string CreateDefaultColumnName()
    {
        return 
            DefaultColumnNamePrefix 
            + new Random((int)DateTime.UtcNow.Ticks).Next(1, 1000);
    }
}
