namespace Nemonuri.AdhocTables;

public class CellConventionBuiler
{
    public CellConventionBuiler()
    {
        IsEmptyable = true;
    }

    public IStringConverter? CanonDataTypeConverter {get;set;}

    public string? EmptyValue {get;set;}

    public bool IsEmptyable {get;set;}

    public CellConvention Build() =>
        new CellConvention(
            canonDataTypeConverter: CanonDataTypeConverter, 
            emptyValue: EmptyValue, 
            isEmptyable: IsEmptyable
        );
}