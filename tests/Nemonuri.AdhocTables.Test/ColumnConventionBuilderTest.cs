namespace Nemonuri.AdhocTables.Test;

using Model;

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
    public void ColumnConventionBuilder__Build_Result__No_Exception(ColumnConventionBuilder builderArg)
    {
        //Model
        ColumnConventionBuilder builder = builderArg;
        
        //Act
        ColumnConvention? columnConvention = builder.Build();

        //Assert
#if WRITE_OUTPUT
        _output.WriteLine(columnConvention.ToString());
#endif
    }
}
