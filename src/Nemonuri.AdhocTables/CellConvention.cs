namespace Nemonuri.AdhocTables;

public record CellConvention
{
    public CellConvention(IStringConverter canonDataTypeConverter, string? emptyValue = null, bool isEmptyable = true)
    {
        Guard.IsNotNull(canonDataTypeConverter);
        CanonDataTypeConverter = canonDataTypeConverter;
        EmptyValue = emptyValue;
        IsEmptyable = isEmptyable;
    }

    public IStringConverter CanonDataTypeConverter {get;}

    public string? EmptyValue {get;}

    public bool IsEmptyable {get;}

    public CellConventionBuiler ToBuilder => new() {
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

public class CellConventionBuiler()
{
    public IStringConverter? CanonDataTypeConverter {get;set;}

    public string? EmptyValue {get;set;}

    public bool IsEmptyable {get;set;}

    [MemberNotNullWhen(true, nameof(CanonDataTypeConverter))]
    public bool IsValidToBuild => CanonDataTypeConverter != null;

    public CellConvention? Build() =>
        IsValidToBuild ? 
            new CellConvention(CanonDataTypeConverter, EmptyValue, IsEmptyable)
            : null;
}