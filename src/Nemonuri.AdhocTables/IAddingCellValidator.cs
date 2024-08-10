namespace Nemonuri.AdhocTables;

public interface IAddingCellValidator
{
    bool IsValidToAppendData(Column column, string data);
}

public static class BuiltInAddingCellValidator
{
    public readonly static IAddingCellValidator TableIdExists = new TableIdExistsAddingCellValidator();

    private class TableIdExistsAddingCellValidator : IAddingCellValidator
    {
        public TableIdExistsAddingCellValidator(){}

        public bool IsValidToAppendData(Column column, string data) =>
            column.AdhocTableContext.IsTableIdExist(data);
    }
}
