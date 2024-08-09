using System.Collections.Concurrent;

namespace Nemonuri.AdhocTables;

public interface IReferenceConvention: IServiceProvider
{
    object? GetByReference(Cell cell);
}

public interface IReferenceConvention<T> : IReferenceConvention
{
    new T? GetByReference(Cell cell);
}
