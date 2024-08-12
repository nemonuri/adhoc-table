namespace Nemonuri.AdhocTables.ComponentModel;

public class ObservableAdhocTable : ObservableObject
{
    public ObservableAdhocTable(AdhocTableBuilder adhocTableBuilder):base()
    {
        Guard.IsNotNull(adhocTableBuilder);
        AdhocTable = adhocTableBuilder.Build();
    }

    public AdhocTable AdhocTable {get;}


}
