namespace Nemonuri.AdhocTables;

public class Cell
{
    private readonly ColumnConvention _columnConvention;
    private readonly CellConvention _cellConvention;
    private readonly string _value;
    private Column? _column;
    private Row? _row;
    

    internal Cell(
        ColumnConvention columnConvention,
        string value
    )
    {
        Guard.IsNotNull(columnConvention);
        Guard.IsNotNull(value);
        _columnConvention = columnConvention;
        _value = value;
        _cellConvention = _columnConvention.CellConvention;
    }

    public ColumnConvention ColumnConvention => _columnConvention;

    public CellConvention CellConvention => _cellConvention;

    public CultureInfo CultureInfo => _columnConvention.CultureInfo;

    public string Value => _value;

    private object? _convertedValue;
    public object ConvertedValue =>
        _cellConvention.CanonDataTypeConverter.TryConvert(_value, CultureInfo, out _convertedValue) ?
        _convertedValue :
        ThrowHelper.ThrowInvalidDataException<object>(/* TODO */);
    
    [MemberNotNullWhen(true, nameof(Column))]
    public bool IsInsertedToColumn => _column != null;
    [MemberNotNullWhen(true, nameof(Column), nameof(Row), nameof(AdhocTable), nameof(AdhocTableContext))]
    public bool IsInsertedToTable => _column != null && _row != null;

    public Column? Column => _column;

    internal void SetColumn(Column? column)
    {
        //TODO: interlocked
        _column = column;
    }

    public Row? Row => _row;

    internal void SetRow(Row? row)
    {
        //TODO: interlocked
        _row = row;
    }

    public AdhocTable? AdhocTable => IsInsertedToTable ? Column.AdhocTable : null;

    public AdhocTableContext? AdhocTableContext => AdhocTable?.AdhocTableContext;

    public IReferenceConvention? ReferenceConvention => _columnConvention.ReferenceConvention;


    public object GetDefaultReference()
    {
        Guard.IsNotNull(ReferenceConvention);
        object? result = ReferenceConvention.GetByReference(this);
        Guard.IsNotNull(result, "ReferenceConvention.GetByReference(this)");
        return result;
    }

    public bool TryGetDefaultReference([NotNullWhen(true)] out object? outResult)
    {
        outResult = ReferenceConvention?.GetByReference(this);
        return outResult != null;
    }

    public T GetDefaultReference<T>() => 
        GetDefaultReference() is T vT ? vT : ThrowHelper.ThrowArrayTypeMismatchException<T>(/* TODO */);
    
    public bool TryGetDefaultReference<T>([NotNullWhen(true)] out T? outResult)
    {
        if (TryGetDefaultReference(out object? vObj) && vObj is T vT)
        {
            outResult = vT;
            return true;
        }
        else
        {
            outResult = default;
            return false;
        }
    }
}