using System.Net.Http;
using System.Threading.Tasks;
using FunctionApp.IntegrationTest.Collections;
using FunctionApp.IntegrationTest.Fixtures;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using TestStack.BDDfy;
using Xunit;

namespace FunctionApp.IntegrationTest.Tests
{
    [Collection(nameof(FunctionTestCollection))]
    public class HelloQueueFunctionTest
    {
        private readonly FunctionTestFixture _fixture;

        private readonly CloudQueue _queue = CloudStorageAccount
            .DevelopmentStorageAccount
            .CreateCloudQueueClient()
            .GetQueueReference("greetings");
        
        public HelloQueueFunctionTest(FunctionTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Hello_Queue_Function_Test()
        {
            this.BDDfy();
        }

        private async Task Given_The_Greetings_Queue_Exists_And_Is_Empty()
        {
            await _queue.CreateIfNotExistsAsync();
            await _queue.ClearAsync();
        }

        private async Task When_The_HelloQueue_Function_Is_Invoked()
        {
            var response = await _fixture.Client.GetAsync("api/HelloQueue?name=James+Bond");
            response.EnsureSuccessStatusCode();
        }

        private async Task Then_The_Enqueued_Message_Should_Include_The_Caller_Name()
        {
            var message = await _queue.GetMessageAsync();
            Assert.EndsWith("James Bond", message.AsString);
        }
    }
}
