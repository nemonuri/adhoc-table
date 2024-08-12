namespace Nemonuri.AdhocTables.BuiltIns;

public static class BuiltInAddingCellValidatorFactory
{
    public readonly static IAddingCellValidatorFactory<string, IAddingCellValidator> TableIdAndPrimaryKeyExist =
        new TableIdAndPrimaryKeyExistAddingCellValidatorFactory();


    private class TableIdAndPrimaryKeyExistAddingCellValidator : IAddingCellValidator
    {
        private readonly string _tableId;

        public TableIdAndPrimaryKeyExistAddingCellValidator(string tableId)
        {
            _tableId = tableId;
        }

        public bool IsValidToAppendData(Column column, string data)
        {
            if (!column.AdhocTableContext.TryGetTableFromId(_tableId, out AdhocTable? table)) {return false;}
            if (!table.HasPrimaryKey) {return false;}
            if (!table.ColumnConventionCollection[table.PrimaryKeyColumnIndex].CellConvention.CanonDataTypeConverter.TargetType.IsEquivalentTo(
                column.CellConvention.CanonDataTypeConverter.TargetType
            )) {return false;}
            return true;
        }
    }

    private class TableIdAndPrimaryKeyExistAddingCellValidatorFactory() : IAddingCellValidatorFactory<string, IAddingCellValidator>
    {
        public IAddingCellValidator? Create(string tableId) => new TableIdAndPrimaryKeyExistAddingCellValidator(tableId);
    }
}

public interface IAddingCellValidatorFactory<TArg, TAddingCellValidator>
    where TAddingCellValidator : IAddingCellValidator
{
    IAddingCellValidator? Create(TArg arg);
}
