using C = Nemonuri.AdhocTables.Test.Model.ColumnConventionBuilderModel;

namespace Nemonuri.AdhocTables.Test.Model;

public static class AdhocTableBuilderModel
{
    public const string RootTableId1 = "Root";

    public static AdhocTableBuilder CreateRidForeignKeyStringValueAdhocTableBuilder() => 
        new AdhocTableBuilder() {
            Id = RootTableId1,
            ColumnConventionCollection = new ([
                C.CreateRidColumnConventionBuilder().Build(),
                C.CreateForeignKeyColumnConventionBuilder().Build(),
                C.CreateStringValueColumnConventionBuilder().Build()
            ])
        };

    public static AdhocTableBuilder CreateRidStringValue1StringValue2AdhocTableBuilder() => 
        new AdhocTableBuilder() {
            Id = RootTableId1,
            ColumnConventionCollection = new ([
                C.CreateRidColumnConventionBuilder().Build(),
                C.CreateStringValueColumnConventionBuilder("StringValue1").Build(),
                C.CreateStringValueColumnConventionBuilder("StringValue2").Build()
            ])
        };

    public static IEnumerable<object[]> GetAdhocTableBuilders()
    {
        yield return [new AdhocTableBuilder()];
        yield return [CreateRidForeignKeyStringValueAdhocTableBuilder()];
        yield return [CreateRidStringValue1StringValue2AdhocTableBuilder()];
    }
}