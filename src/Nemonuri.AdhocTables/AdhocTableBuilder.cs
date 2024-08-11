namespace Nemonuri.AdhocTables;

public class AdhocTableBuilder()
{
    public string? Id {get;set;}
    public ColumnConventionCollection? ColumnConventionCollection {get;set;}
    public AdhocTableContext? AdhocTableContext {get;set;}

    public AdhocTable Build() => 
        new AdhocTable(
            id: Id, 
            columnConventionCollection: ColumnConventionCollection, 
            adhocTableContext: AdhocTableContext
        );
}
