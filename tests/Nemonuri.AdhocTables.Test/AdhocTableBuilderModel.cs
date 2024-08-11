using C = Nemonuri.AdhocTables.Test.ColumnConventionBuilderModel;

namespace Nemonuri.AdhocTables.Test;

public static class AdhocTableBuilderModel
{
    public const string RootTableId1 = "Root";

    public static AdhocTableBuilder CreateAdhocTableBuilder() => 
        new AdhocTableBuilder() {
            Id = RootTableId1,
            ColumnConventionCollection = new ([
                C.CreateRidColumnConventionBuilder().Build(),
                C.CreateForeignKeyColumnConventionBuilder().Build(),
                C.CreateStringValueColumnConventionBuilder().Build()
            ])
        };

    public static IEnumerable<object[]> GetAdhocTableBuilders()
    {
        yield return [new AdhocTableBuilder()];
        yield return [CreateAdhocTableBuilder()];
    }
}