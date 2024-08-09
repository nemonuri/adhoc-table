
namespace Nemonuri.AdhocTables;

public static class BuiltInReferenceConventionFactory
{

    public class TableAndPrimeKeyToRowReferenceConventionFactory
    {
        private class TableAndPrimeKeyToRowReferenceConvention : IReferenceConvention<Row>
        {
            public Row? GetByReference(Cell cell)
            {
                throw new NotImplementedException();
            }

            public object? GetService(Type serviceType)
            {
                if (BuiltInReferenceConventionService.AddingCellValidators.IsAssignableFrom(serviceType))
                {
                    //return 
                }
                return null;
            }

            object? IReferenceConvention.GetByReference(Cell cell)
            {
                throw new NotImplementedException();
            }
        }
    }
}
