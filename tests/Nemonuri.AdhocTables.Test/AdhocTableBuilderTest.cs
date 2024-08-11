namespace Nemonuri.AdhocTables.Test;

using Model;

public class AdhocTableBuilderTest
{
    private readonly ITestOutputHelper _output;

    public AdhocTableBuilderTest(ITestOutputHelper output)
    {
        _output = output;
    }

    public static IEnumerable<object[]> GetAdhocTableBuilders() => AdhocTableBuilderModel.GetAdhocTableBuilders();

    [Theory]
    [MemberData(nameof(GetAdhocTableBuilders))]
    public void AdhocTableBuilder__Build_Result__No_Exception(AdhocTableBuilder builderArg)
    {
        //Model
        builderArg.AdhocTableContext = new AdhocTableContext();
        AdhocTableBuilder builder = builderArg;

        //Act
        AdhocTable? adhocTable = builder.Build();

        //Assert
#if WRITE_OUTPUT
        _output.WriteLine(adhocTable.ToString()); //TODO: Make debugger view method
#endif
    }
}