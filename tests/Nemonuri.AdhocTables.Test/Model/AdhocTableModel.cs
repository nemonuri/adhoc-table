using Ab = Nemonuri.AdhocTables.Test.Model.AdhocTableBuilderModel;

namespace Nemonuri.AdhocTables.Test.Model;

public static class AdhocTableModel
{
    public static AdhocTable CreateRidForeignKeyStringValueAdhocTable(AdhocTableContext context)
    {
        var builder = Ab.CreateRidForeignKeyStringValueAdhocTableBuilder();
        builder.AdhocTableContext = context;
        return builder.Build();
    }

    public static AdhocTable CreateRidStringValue1StringValue2AdhocTable(AdhocTableContext context)
    {
        var builder = Ab.CreateRidStringValue1StringValue2AdhocTableBuilder();
        builder.AdhocTableContext = context;
        return builder.Build();
    }
}