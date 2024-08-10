using System.Collections;

namespace Nemonuri.AdhocTables;

public class Row : IReadOnlyList<Cell>
{
    private readonly Cell[] _cells;

    internal Row(Cell[] cells)
    {
        //TODO: validate
        _cells = cells;
        foreach (var c in _cells)
        {
            c.SetRow(this);
        }
    }

    public AdhocTable? AdhocTable => (Count > 0 && _cells[0].IsInsertedToTable) ? _cells[0].AdhocTable : null;

    public Cell this[int index] => _cells[index];

    public int Count => _cells.Length;

    public IEnumerator<Cell> GetEnumerator() => (_cells as IReadOnlyList<Cell>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _cells.GetEnumerator();
}