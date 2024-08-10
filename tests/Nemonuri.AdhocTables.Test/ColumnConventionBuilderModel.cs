namespace Nemonuri.AdhocTables.Test;

public static class ColumnConventionBuilderModel
{
    public static ColumnConventionBuilder CreateRidColumnConventionBuilder() => 
        new ColumnConventionBuilder()   
        {
            ColumnName = "RID",
            CellConvention = BuiltInCellConvention.UInt32WithoutZero,
            IsUnique = true,
            IsPrimaryKey = true
        };

    public static ColumnConventionBuilder CreateForeignKeyColumnConventionBuilder(string? columnName = null, string? tableId = null) => 
        new ColumnConventionBuilder()
        {
            ColumnName = columnName ?? "ForeignId",
            CellConvention = BuiltInCellConvention.UInt32WithoutZero,
            IsUnique = false,
            IsPrimaryKey = false,
            ReferenceConvention = BuiltInReferenceConventionFactory.TableAndPrimeKeyToRow.Create(tableId ?? AdhocTableBuilderModel.RootTableId1)
        };

    public static ColumnConventionBuilder CreateStringValueColumnConventionBuilder() =>
        new ColumnConventionBuilder()
        {
            ColumnName = "StringValue",
            CellConvention = BuiltInCellConvention.TrimmedString
        };

    public static IEnumerable<object[]> GetColumnConventionBuilders()
    {
        yield return [CreateRidColumnConventionBuilder()];
        yield return [CreateForeignKeyColumnConventionBuilder()];
        yield return [CreateStringValueColumnConventionBuilder()];
    }
}
