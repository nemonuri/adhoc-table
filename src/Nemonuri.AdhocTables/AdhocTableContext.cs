using System.Collections.Concurrent;

namespace Nemonuri.AdhocTables;

public class AdhocTableContext
{
#region copy code
/*
Origianl:
https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Runtime/Loader/AssemblyLoadContext.cs
*/

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

    private static volatile Dictionary<long, WeakReference<AdhocTableContext>>? s_allContexts;
    private static long s_nextId = 0;

    [MemberNotNull(nameof(s_allContexts))]
    private static Dictionary<long, WeakReference<AdhocTableContext>> AllContexts =>
        s_allContexts ??
        Interlocked.CompareExchange(ref s_allContexts, new Dictionary<long, WeakReference<AdhocTableContext>>(), null) ??
        s_allContexts;

#endregion copy code

    private static readonly AdhocTableContext s_default = new AdhocTableContext("Default", false);
    public static AdhocTableContext Default => s_default;

    private readonly ConcurrentDictionary<string, AdhocTable> _tableDictionary;
    private readonly long _id;
    private readonly string? _name;
    private readonly bool _isCollectible;

    public AdhocTableContext(string? name = null):this(name, true)
    {
    }

    private AdhocTableContext(string? name, bool isCollectible) 
    {
        _tableDictionary = new ConcurrentDictionary<string, AdhocTable>();
        _name = name;
        _isCollectible = isCollectible;

        Dictionary<long, WeakReference<AdhocTableContext>> allContexts = AllContexts;
        lock (allContexts)
        {
            _id = s_nextId++;
            allContexts.Add(_id, new WeakReference<AdhocTableContext>(this, false));
        }
    }    

    public string? Name => _name;

    public bool IsTableIdExist(string id) => _tableDictionary.ContainsKey(id);

    public bool TryGetTableFromId(string id, [NotNullWhen(true)] out AdhocTable? table) =>
        _tableDictionary.TryGetValue(id, out table);

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
        if (_tableDictionary.ContainsKey(id!))
        {
            return !throwOnError && ThrowHelper.ThrowInvalidOperationException<bool>(/* TODO */);
        }
        return true;
    }
}