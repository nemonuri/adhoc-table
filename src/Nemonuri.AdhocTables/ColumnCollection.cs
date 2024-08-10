using System.Collections;

namespace Nemonuri.AdhocTables;

public class ColumnCollection : IReadOnlyList<Column>
{
    private readonly AdhocTable _adhocTable;
    private readonly Column?[] _columns; //TODO: sync. https://github.com/dotnet/wcf/blob/main/src/System.ServiceModel.Primitives/src/System/ServiceModel/SynchronizedCollection.cs

    internal ColumnCollection(AdhocTable adhocTable)
    {
        Guard.IsNotNull(adhocTable);
        _adhocTable = adhocTable;
        _columns = new Column?[adhocTable.ColumnCount];
    }

    public Column this[int index] => GetChild(index);

    public AdhocTable AdhocTable => _adhocTable;

    public int Count => _columns.Length;

    public IEnumerator<Column> GetEnumerator()
    {
        for (int i=0;i < Count;i++)
        {
            yield return this[i];
        }
    }

    internal Column GetChild(int index)
    {
        var item = _columns[index];
        if (item != null) {return item;}

        item = new Column(_adhocTable, index);
        _columns[index] = item;

        return item;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private int? _primaryKeyIndex;
    public int PrimaryKeyIndex => _primaryKeyIndex ??= GetFirstIndex(static c => c.IsPrimaryKey);

    internal int GetFirstIndex(Func<Column, bool> predicate)
    {
        Guard.IsNotNull(predicate);
        for (int i=0;i < Count;i++)
        {
            if (predicate(this[i]))
            {
                return i;
            }
        }
        return -1;
    }
}
