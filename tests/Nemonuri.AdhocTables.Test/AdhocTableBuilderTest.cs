namespace Nemonuri.AdhocTables.Test;

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
    public void AdhocTableBuilder__Build_Result__Is_Not_Null(AdhocTableBuilder builderArg)
    {
        //Model
        builderArg.AdhocTableContext = new AdhocTableContext();
        AdhocTableBuilder builder = builderArg;

        //Act
        AdhocTable? adhocTable = builder.Build();

        //Assert
        Assert.NotNull(adhocTable);
    }
}
