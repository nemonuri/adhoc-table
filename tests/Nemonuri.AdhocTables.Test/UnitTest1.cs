namespace Nemonuri.AdhocTables.Test;

public class UnitTest1
{
    public static ColumnConventionBuilder CreateRidColumnConventionBuilder() => new ColumnConventionBuilder()
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
            IsUnique = true,
            IsPrimaryKey = true,
            ReferenceConvention = BuiltInReferenceConventionFactory.TableAndPrimeKeyToRow.Create(tableId ?? "Root")
        };

    public static IEnumerable<object[]> GetColumnConventionBuilders()
    {
        yield return [CreateRidColumnConventionBuilder()];
    }
    

    [Theory]
    [MemberData(nameof(GetColumnConventionBuilders))]
    public void ColumnConventionBuilder__Build_Result__Is_Not_Null(ColumnConventionBuilder builderArg)
    {
        //Model
        ColumnConventionBuilder builder = builderArg;
        
        //Act
        ColumnConvention? columnConvention = builder.Build();

        //Assert
        Assert.NotNull(columnConvention);
    }
}