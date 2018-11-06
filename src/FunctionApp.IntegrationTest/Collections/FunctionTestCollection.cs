using FunctionApp.IntegrationTest.Fixtures;
using Xunit;

namespace FunctionApp.IntegrationTest.Collections
{
    [CollectionDefinition(nameof(FunctionTestCollection))]
    public class FunctionTestCollection : ICollectionFixture<FunctionTestFixture>
    {
    }
}
