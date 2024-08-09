namespace Nemonuri.AdhocTables;

public record ColumnConvention
{
    public ColumnConvention(
        string columnName, 
        CellConvention cellConvention,
        bool isUnique = false,
        bool isPrimaryKey = false,
        CultureInfo? cultureInfo = null,
        IReferenceConvention? referenceConvention = null)
    {
        Guard.IsNotNull(columnName);
        Guard.IsNotNull(cellConvention);
        ColumnName = columnName;
        CellConvention = cellConvention;
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
}

public class ColumnConventionBuilder()
{
    public string? ColumnName {get;set;}
    public CellConvention? CellConvention {get;set;}
    public bool IsUnique {get;set;}
    public bool IsPrimaryKey {get;set;}
    public CultureInfo? CultureInfo {get;set;}
    public IReferenceConvention? ReferenceConvention {get;set;}

    [MemberNotNullWhen(true, nameof(ColumnName), nameof(CellConvention))]
    public bool IsValidToBuild => ColumnName != null && CellConvention != null;

    public ColumnConvention? Build() => 
        IsValidToBuild 
            ? new ColumnConvention(
                ColumnName, 
                CellConvention,
                IsUnique,
                IsPrimaryKey,
                CultureInfo,
                ReferenceConvention
              )
            : null;
}