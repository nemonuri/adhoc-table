namespace Nemonuri.AdhocTables.Test;

using Model;

public class AdhocTableTest
{
    private readonly ITestOutputHelper _output;

    public AdhocTableTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(0, "asdf", "qwqw")]
    [InlineData(1, "ejiji", "aaaaaaa")]
    [InlineData(2, "b2", "b2_1")]
    public void RidStringValue1StringValue2AdhocTable_With_Added_Rows__Forall_Row_Get_By_Index__Result_Is_Equal_To_Get_By_Added_Order
    (
        int rowIndex,
        string expected1,
        string expected2
    )
    {
        //Model
        var adhocTable = AdhocTableModel.CreateRidStringValue1StringValue2AdhocTable(new AdhocTableContext());
        adhocTable.Add(["1", "asdf", "qwqw"]);
        adhocTable.Add(["85", "ejiji", "aaaaaaa"]);
        adhocTable.Add(["2", "b2", "b2_1"]);

        //Act
        string actual1 = adhocTable.Rows[rowIndex][1].Value;
        string actual2 = adhocTable.Rows[rowIndex][2].Value;

        //Assert
        Assert.Equal(expected1, actual1);
        Assert.Equal(expected2, actual2);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 85)]
    [InlineData(2, 2)]
    public void RidStringValue1StringValue2AdhocTable_With_Added_Rows__Forall_Row_Get_By_Index__Result_Is_Equal_To_Get_By_Rid_Dictonary
    (
        int rowIndex,
        uint rid
    )
    {
        //Model
        var adhocTable = AdhocTableModel.CreateRidStringValue1StringValue2AdhocTable(new AdhocTableContext());
        adhocTable.Add(["1", "asdf", "qwqw"]);
        adhocTable.Add(["85", "ejiji", "aaaaaaa"]);
        adhocTable.Add(["2", "b2", "b2_1"]);
        string expected1 = adhocTable.Rows[rowIndex][1].Value;
        string expected2 = adhocTable.Rows[rowIndex][2].Value;

        //Act
        string actual1 = adhocTable.RowDictionary?[rid][1].Value ?? string.Empty;
        string actual2 = adhocTable.RowDictionary?[rid][2].Value ?? string.Empty;

        //Assert
        Assert.Equal(expected1, actual1);
        Assert.Equal(expected2, actual2);
    }
}
