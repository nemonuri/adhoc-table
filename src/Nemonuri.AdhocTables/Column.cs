namespace Nemonuri.AdhocTables;

public class Column
{
    private readonly AdhocTable _adhocTable;
    private readonly int _columnIndex;
    private readonly ColumnConvention _columnConvention;
    private readonly List<Cell> _cells;

    internal Column(AdhocTable adhocTable, int columnIndex)
    {
        //TODO: null check
         _adhocTable = adhocTable;
         _columnIndex = columnIndex;
         _columnConvention = _adhocTable.ColumnConventionCollection[columnIndex];
         _cells = new List<Cell>();
    }

    public AdhocTable AdhocTable => _adhocTable;

    public AdhocTableContext AdhocTableContext => _adhocTable.AdhocTableContext;

    public int ColumnIndex => _columnIndex;

    public ColumnConvention ColumnConvention => _columnConvention;

    public string Name => _columnConvention.ColumnName;

    internal CellConvention CellConvention => _columnConvention.CellConvention;

    internal IStringConverter CanonDataTypeConverter => CellConvention.CanonDataTypeConverter;

    public CultureInfo CultureInfo => _columnConvention.CultureInfo;

    [MemberNotNullWhen(true, nameof(UniqueDictionaryInternal))]
    public bool IsUnique => _columnConvention.IsUnique;

    [MemberNotNullWhen(true, nameof(UniqueDictionaryInternal))] // TODO: support multi column primary key
    public bool IsPrimaryKey => 
        _columnConvention.IsUnique && 
        _columnConvention.IsPrimaryKey && 
        _columnConvention.CellConvention.EmptyValue != null &&
        !_columnConvention.CellConvention.IsEmptyable;

    private Dictionary<object, Cell>? _uniqueDictionary;
    internal Dictionary<object, Cell>? UniqueDictionaryInternal =>
        _uniqueDictionary ??= (IsUnique ? new Dictionary<object, Cell>() : null);
    public IReadOnlyDictionary<object, Cell>? UniqueDictionary => UniqueDictionaryInternal;
    
    
    public IReadOnlyList<Cell> Cells => _cells;

    internal Cell? CreateInsertableCellOrNull(string value)
    {
        if (!IsValidDataToInsert(value)) {return null;}
        return new Cell(ColumnConvention, value);
    }

    internal bool IsValidDataToInsert(string value)
    {
        if (value == null) {return false;}

        //--- by CellConvention ---
        if (!CanonDataTypeConverter.TryConvert(value, CultureInfo, out object? convertedValue))
        {
            return false;
        }

        if (CellConvention.EmptyValue != null && !CellConvention.IsEmptyable)
        {
            object? convertedEmpty = CellConvention.CreateConvertedEmptyValueOrNull(CultureInfo);
            if (convertedValue.Equals(convertedEmpty))
            {
                return false;
            }
        }
        //---|

        if (IsUnique)
        {
            if (UniqueDictionaryInternal.ContainsKey(convertedValue)) {return false;}
        }

        if (_columnConvention.ReferenceConvention?.GetService(BuiltInReferenceConventionService.AddingCellValidators) 
            is IEnumerable<IAddingCellValidator> acvs)
        {
            foreach (IAddingCellValidator acv in acvs)
            {
                if (!acv.IsValidToAppendData(this, value)) {return false;}
            }
        }

        return true;
    }

    internal bool Add(Cell cell)
    {
        if (cell == null) {return false;}
        if (cell.ColumnConvention != ColumnConvention) {return false;}
        if (cell.IsInsertedToColumn) {return false;}

        cell.SetColumn(this);
        _cells.Add(cell);
        if (IsUnique)
        {
            UniqueDictionaryInternal.Add(cell.ConvertedValue, cell);
        }

        return true;
    }

    internal bool RemoveLast()
    {
        Cell? removingCell = _cells.LastOrDefault();
        if (removingCell == null) {return false;}

        removingCell.SetColumn(null);
        _cells.Remove(removingCell);
        if (IsUnique)
        {
            UniqueDictionaryInternal.Remove(removingCell.ConvertedValue);
        }

        return true;
    }

}
