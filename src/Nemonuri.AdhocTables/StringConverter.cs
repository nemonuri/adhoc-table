namespace Nemonuri.AdhocTables;

public interface IStringConverter
{
    public Type TargetType {get;}

    public bool TryConvert(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out object? result);

    public bool TryConvertBack(object value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result);
}

public interface IStringConverter<TTarget>
{
    public bool TryConvert(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out TTarget? result);

    public bool TryConvertBack(TTarget value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result);
}

public abstract class StringConverterBase<TTarget> : IStringConverter<TTarget>, IStringConverter
{
    protected StringConverterBase() {}

    public Type TargetType => typeof(TTarget);

    public abstract bool TryConvert(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out TTarget? result);

    public abstract bool TryConvertBack(TTarget value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result);

    bool IStringConverter.TryConvert(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out object? result)
    {
        bool success = TryConvert(value, cultureInfo, out TTarget? vTarget);
        result = success ? vTarget : null;
        return success;
    }

    bool IStringConverter.TryConvertBack(object value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result)
    {
        result = null;
        return value is TTarget vTarget && TryConvertBack(vTarget, cultureInfo, out result);
    }        
}