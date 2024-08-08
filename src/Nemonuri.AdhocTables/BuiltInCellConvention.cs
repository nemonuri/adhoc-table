namespace Nemonuri.AdhocTables;

public static class BuiltInCellConvention
{
    public readonly static CellConvention String = new (BuiltInStringConverter.StringToString);
    public readonly static CellConvention TrimmedString = new (BuiltInStringConverter.StringToTrimmedString);
    public readonly static CellConvention TrimmedStringWithoutEmpty = new (BuiltInStringConverter.StringToTrimmedString, string.Empty, false);
    public readonly static CellConvention Int32 = new (BuiltInStringConverter.StringToInt32);
    public readonly static CellConvention UInt32 = new (BuiltInStringConverter.StringToUInt32);
    public readonly static CellConvention UInt32WithoutZero = new (BuiltInStringConverter.StringToUInt32, "0", false);
}
