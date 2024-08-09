namespace Nemonuri.AdhocTables;

public interface IAddingCellValidator
{
    bool IsValidToAppendData(Column column, string data);
}