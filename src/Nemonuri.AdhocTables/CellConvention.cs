namespace Nemonuri.AdhocTables;

public record CellConvention
{
    public CellConvention(
        IStringConverter? canonDataTypeConverter = null,
        string? emptyValue = null, 
        bool isEmptyable = true
    )
    {
        CanonDataTypeConverter = canonDataTypeConverter ?? BuiltInStringConverter.StringToString;
        EmptyValue = emptyValue;
        IsEmptyable = isEmptyable;
    }

    public IStringConverter CanonDataTypeConverter {get;}

    public string? EmptyValue {get;}

    public bool IsEmptyable {get;}

    public CellConventionBuiler ToBuilder() => new() {
        CanonDataTypeConverter = CanonDataTypeConverter,
        EmptyValue = EmptyValue,
        IsEmptyable = IsEmptyable
    };

    internal object? CreateConvertedEmptyValueOrNull(CultureInfo? cultureInfo = null)
    {
        var cul = cultureInfo ?? CultureInfo.InvariantCulture;

        if (EmptyValue == null)
        {
            return null;
        }

        if (CanonDataTypeConverter.TryConvert(EmptyValue, cul, out object? result))
        {
            return result;
        }
        return null;
    }
}
