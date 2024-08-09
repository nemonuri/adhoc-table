namespace Nemonuri.AdhocTables;

public record AdhocTable
{
    public static AdhocTableBuilder CreateBuilder() => new();

    internal AdhocTable(
        string id,
        ColumnConventionCollection columnConventionCollection,
        AdhocTableContext? adhocTableContext = null)
    {
        IsValidIdToBuild(id, true);
        Guard.IsNotNull(columnConventionCollection);

        Id = id;
        _columnConventionCollection = columnConventionCollection;
        _adhocTableContext = adhocTableContext ?? AdhocTableContext.Default;

        AdhocTableContext.Default.AddTable(this);
    }

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
    public ColumnCollection ColumnCollection => _columnCollection ?? new ColumnCollection(this);

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
        }

        return true;
    }
}

public class AdhocTableBuilder()
{
    public string? Id {get;set;}
    public ColumnConventionCollection? ColumnConventionCollection {get;set;}
    public AdhocTableContext? AdhocTableContext {get;set;}

    [MemberNotNullWhen(true, nameof(Id), nameof(ColumnConventionCollection))]
    public bool IsValidToBuild => 
        AdhocTable.IsValidIdToBuild(Id, false) && ColumnConventionCollection != null;
    
    public AdhocTable? Build() => 
        IsValidToBuild ? new(Id, ColumnConventionCollection, AdhocTableContext) : null;
}
