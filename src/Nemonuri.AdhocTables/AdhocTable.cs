namespace Nemonuri.AdhocTables;

public record AdhocTable
{
    public static AdhocTableBuilder CreateBuilder() => new();

    private static ColumnConventionCollection? s_defaultColumnConvention;
    internal static ColumnConventionCollection DefaultColumnConvention =>
        s_defaultColumnConvention ??
        Interlocked.CompareExchange(ref s_defaultColumnConvention, new ColumnConventionCollection([new ColumnConvention()]), null) ??
        s_defaultColumnConvention;

    private readonly List<Row> _rows;

    public AdhocTable(
        string? id = null,
        ColumnConventionCollection? columnConventionCollection = null,
        AdhocTableContext? adhocTableContext = null)
    {
        _columnConventionCollection = columnConventionCollection ?? DefaultColumnConvention;
        _adhocTableContext = adhocTableContext ?? AdhocTableContext.Default;
        _rows = new();
        Id = id ?? CreateDefaultTableId(_adhocTableContext);

        _adhocTableContext.AddTable(this);
    }

    public AdhocTableBuilder ToBuilder() =>
        new () {
            Id = Id,
            ColumnConventionCollection = ColumnConventionCollection,
            AdhocTableContext = AdhocTableContext
        };

    public string Id {get;}
    internal static bool IsValidIdToBuild(string? id, bool throwOnError)
    {
        if (!AdhocTableContext.Default.IsValidIdToAdd(id, throwOnError)) {return false;}
        return true;
    }

    private readonly ColumnConventionCollection _columnConventionCollection;
    internal ColumnConventionCollection ColumnConventionCollection => _columnConventionCollection;

    public int ColumnCount => _columnConventionCollection.Count;

    private readonly AdhocTableContext _adhocTableContext;
    public AdhocTableContext AdhocTableContext => _adhocTableContext;

    private ColumnCollection? _columnCollection;
    public ColumnCollection ColumnCollection => _columnCollection ??= new ColumnCollection(this);

    public IReadOnlyList<Row> Rows => _rows;

    [MemberNotNullWhen(true, nameof(RowDictionaryInternal), nameof(RowDictionary))]
    public bool HasPrimaryKey => PrimaryKeyColumnIndex >= 0;

    public int PrimaryKeyColumnIndex => ColumnCollection.PrimaryKeyIndex;

    private Dictionary<object, Row>? _rowDictionary;
    internal Dictionary<object, Row>? RowDictionaryInternal =>
        _rowDictionary ??= (HasPrimaryKey ? new Dictionary<object, Row>() : null);
    public IReadOnlyDictionary<object, Row>? RowDictionary => RowDictionaryInternal;

    public bool Add(ReadOnlySpan<string> strings)
    {
        if (strings.Length != ColumnCount) {return false;}
        Cell[] insertableCells = new Cell[strings.Length];
        for(int i=0;i<strings.Length;i++)
        {
            Cell? c = ColumnCollection[i].CreateInsertableCellOrNull(strings[i]);
            if (c == null) {return false;}
            insertableCells[i] = c;
        }

        int j = 0;
        bool successed = true;
        for (;j < insertableCells.Length;j++)
        {
            successed = ColumnCollection[j].Add(insertableCells[j]);
            if (!successed) {break;}
        }

        if (!successed)
        {
            for (j--;j >= 0;j--)
            {
                ColumnCollection[j].RemoveLast();
            }
            return false;
        }

        var addingRow = new Row(insertableCells);
        _rows.Add(addingRow);
        if (HasPrimaryKey)
        {
            RowDictionaryInternal.Add(addingRow[PrimaryKeyColumnIndex].ConvertedValue, addingRow);
        }
        return true;
    }

    private static string CreateDefaultTableId(AdhocTableContext context)
    {
        while (true)
        {
            string resultCandidate = Guid.NewGuid().ToString();
            if(!context.IsTableIdExist(resultCandidate))
            {
                return resultCandidate;
            }
        }
        
    }
}
