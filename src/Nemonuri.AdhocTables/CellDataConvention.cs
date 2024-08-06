namespace Nemonuri.AdhocTables;

public record CellDataConvention
{
    public CellDataConvention(IStringConverter canonDataTypeConverter)
    {
        Guard.IsNotNull(canonDataTypeConverter);
        CanonDataTypeConverter = canonDataTypeConverter;
    }

    public IStringConverter CanonDataTypeConverter {get;}
}
