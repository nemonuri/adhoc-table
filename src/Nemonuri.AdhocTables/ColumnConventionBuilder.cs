namespace Nemonuri.AdhocTables;

public class ColumnConventionBuilder()
{
    public string? ColumnName {get;set;}
    public CellConvention? CellConvention {get;set;}
    public bool IsUnique {get;set;}
    public bool IsPrimaryKey {get;set;}
    public CultureInfo? CultureInfo {get;set;}
    public IReferenceConvention? ReferenceConvention {get;set;}

    public ColumnConvention Build() => 
        new ColumnConvention(
          columnName: ColumnName, 
          cellConvention: CellConvention,
          isUnique: IsUnique,
          isPrimaryKey: IsPrimaryKey,
          cultureInfo: CultureInfo,
          referenceConvention: ReferenceConvention
        );
}