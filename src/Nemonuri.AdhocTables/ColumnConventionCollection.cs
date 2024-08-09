using System.Collections;

namespace Nemonuri.AdhocTables;

public class ColumnConventionCollection : IEquatable<ColumnConventionCollection>, IReadOnlyList<ColumnConvention>
{
    private readonly IReadOnlyList<ColumnConvention> _columnConventions;

    public ColumnConventionCollection(IReadOnlyList<ColumnConvention> columnConventions)
    {
        Guard.IsNotNull(columnConventions);
        Guard.IsFalse(columnConventions.Contains(null));
        _columnConventions = columnConventions;
    }

    public ColumnConvention this[int index] => _columnConventions[index];

    public int Count => _columnConventions.Count;

    public bool Equals(ColumnConventionCollection? other)
    {
        if (other == null) {return false;}
        return _columnConventions.SequenceEqual(other._columnConventions);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ColumnConventionCollection);
    }

    public IEnumerator<ColumnConvention> GetEnumerator() => _columnConventions.GetEnumerator();

    public override int GetHashCode()
    {
        HashCode hashCode = new ();
        hashCode = _columnConventions.Aggregate(hashCode, (gseed, item) => {
            gseed.Add(item);
            return gseed;
        });
        return hashCode.ToHashCode();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
