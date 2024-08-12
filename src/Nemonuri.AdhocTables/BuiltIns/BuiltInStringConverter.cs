


namespace Nemonuri.AdhocTables.BuiltIns;

public static class BuiltInStringConverter
{
    public readonly static IStringConverter<string> StringToString = new StringToStringConverter();
    public readonly static IStringConverter<string> StringToTrimmedString = new StringToTrimmedStringConverter();
    public readonly static IStringConverter<int> StringToInt32 = new StringToInt32Converter();
    public readonly static IStringConverter<uint> StringToUInt32 = new StringToUInt32Converter();


    private class StringToStringConverter() : StringConverterBase<string>
    {
        public override bool TryConvert(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result) =>
            (result = value) is not null;

        public override bool TryConvertBack(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result) =>
            (result = value) is not null;
    }

    private class StringToTrimmedStringConverter() : StringConverterBase<string>
    {
        public override bool TryConvert(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result) =>
            (result = value?.Trim()) is not null;

        public override bool TryConvertBack(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result) =>
            (result = value) is not null;
    }

    private class StringToInt32Converter() : StringConverterBase<int>
    {
        /*
        https://learn.microsoft.com/en-us/dotnet/api/system.globalization.numberstyles?view=netstandard-2.0
        */

        public override bool TryConvert(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out int result) =>
            int.TryParse(value, NumberStyles.Any, cultureInfo, out result) ||
            int.TryParse(value, NumberStyles.HexNumber, cultureInfo, out result);

        public override bool TryConvertBack(int value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result)
        {
            result = value.ToString(cultureInfo);
            return true;
        }
    }

    private class StringToUInt32Converter() : StringConverterBase<uint>
    {
        public override bool TryConvert(string value, CultureInfo? cultureInfo, [NotNullWhen(true)] out uint result) =>
            uint.TryParse(value, NumberStyles.Any, cultureInfo, out result) ||
            uint.TryParse(value, NumberStyles.HexNumber, cultureInfo, out result);

        public override bool TryConvertBack(uint value, CultureInfo? cultureInfo, [NotNullWhen(true)] out string? result)
        {
            result = value.ToString(cultureInfo);
            return true;
        }
    }
}