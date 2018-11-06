using System.Net.Http;
using System.Threading.Tasks;
using FunctionApp.IntegrationTest.Collections;
using FunctionApp.IntegrationTest.Fixtures;
using TestStack.BDDfy;
using Xunit;

namespace FunctionApp.IntegrationTest.Tests
{
    [Collection(nameof(FunctionTestCollection))]
    public class HelloWorldFunctionTest
    {
        private readonly FunctionTestFixture _fixture;
        private HttpResponseMessage _response;

        public HelloWorldFunctionTest(FunctionTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Hello_World_Function_Test()
        {
            this.BDDfy();
        }

        private async Task When_The_HelloWorld_Function_Is_Invoked()
        {
            _response = await _fixture.Client.GetAsync("api/HelloWorld?name=James+Bond");
        }

        private async Task Then_The_Response_Should_Include_The_Caller_Name()
        {
            Assert.EndsWith("James Bond", await _response.Content.ReadAsStringAsync());
        }
    }
}
