namespace Nemonuri.AdhocTables.Test;

public class ColumnConventionBuilderTest
{
    private readonly ITestOutputHelper _output;

    public ColumnConventionBuilderTest(ITestOutputHelper output)
    {
        _output = output;
    }

    public static IEnumerable<object[]> GetColumnConventionBuilders() => ColumnConventionBuilderModel.GetColumnConventionBuilders();

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
#if WRITE_OUTPUT
        _output.WriteLine(columnConvention.ToString());
#endif
    }
}
