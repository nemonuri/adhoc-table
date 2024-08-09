using System.Collections.Concurrent;

namespace Nemonuri.AdhocTables;

public class AdhocTableContext
{
    private static readonly AdhocTableContext s_default = new AdhocTableContext();
    public static AdhocTableContext Default => s_default;

    private readonly ConcurrentDictionary<string, AdhocTable> _tableDictionary;

    private AdhocTableContext() 
    {
        _tableDictionary = new ConcurrentDictionary<string, AdhocTable>();
    }    

    internal void AddTable(AdhocTable table)
    {
        Guard.IsNotNull(table);
        Guard.IsNotNullOrEmpty(table.Id);
        if (!_tableDictionary.TryAdd(table.Id, table))
        {
            throw new InvalidOperationException($"Already exist: {table.Id}");
        }
    }

    internal bool IsValidIdToAdd(string? id, bool throwOnError)
    {
        if (string.IsNullOrEmpty(id))
        {
            return !throwOnError && ThrowHelper.ThrowInvalidOperationException<bool>(/* TODO */);
        }
        if (_tableDictionary.ContainsKey(id))
        {
            return !throwOnError && ThrowHelper.ThrowInvalidOperationException<bool>(/* TODO */);
        }
        return true;
    }
}