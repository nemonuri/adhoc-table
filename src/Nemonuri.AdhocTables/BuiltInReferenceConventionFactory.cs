
namespace Nemonuri.AdhocTables;

public static class BuiltInReferenceConventionFactory
{
    public static readonly IReferenceConventionFactory<string, IReferenceConvention<Row>> TableAndPrimeKeyToRow = new TableAndPrimeKeyToRowReferenceConventionFactory();
        

    private class TableAndPrimeKeyToRowReferenceConvention : IReferenceConvention<Row>
    {
        private readonly string _tableId;

        public TableAndPrimeKeyToRowReferenceConvention(string tableId)
        {
            Guard.IsNotNull(tableId);
            _tableId = tableId;
        }

        public Row? GetByReference(Cell cell)
        {
            if(!cell.IsInsertedToTable) {return null;}
            if(!cell.AdhocTable.AdhocTableContext.TryGetTableFromId(_tableId, out var foreignTable)) {return null;}
            if (!foreignTable.HasPrimaryKey) {return null;}
            if (!foreignTable.RowDictionary.TryGetValue(cell.ConvertedValue, out Row? resultRow)) {return null;}
            return resultRow;
        }

        public object? GetService(Type serviceType)
        {
            if (BuiltInReferenceConventionService.AddingCellValidators == serviceType)
            {
                return new IAddingCellValidator[]{BuiltInAddingCellValidatorFactory.TableIdAndPrimaryKeyExist.Create(_tableId)!};
            }
            return null;
        }

        object? IReferenceConvention.GetByReference(Cell cell) => GetByReference(cell);
    }

    private class TableAndPrimeKeyToRowReferenceConventionFactory() : IReferenceConventionFactory<string, IReferenceConvention<Row>>
    {
        public IReferenceConvention<Row>? Create(string tableId) => new TableAndPrimeKeyToRowReferenceConvention(tableId);
    }
}

public interface IReferenceConventionFactory<TArg, TReferenceConvention>
    where TReferenceConvention : IReferenceConvention
{
    TReferenceConvention? Create(TArg arg);
}