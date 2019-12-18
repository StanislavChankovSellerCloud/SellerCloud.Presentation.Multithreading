using System.Threading.Tasks;
using Xunit;

namespace SellerCloud.Presentation.Multithreading.Tests
{
    [Collection(Traits.INTEGRATION_TESTS)]
    public class AspNetCoreValuesControllerTests
    {
        private readonly IntegrationTestsFixture _fixture;

        private const string VALUES_CONTROLLER = "api/values";

        public AspNetCoreValuesControllerTests(IntegrationTestsFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task GetSynchronizationContext_NoSynchronizationContext()
        {
            var response = await _fixture.Client.GetAsync(VALUES_CONTROLLER);

            response.EnsureSuccessStatusCode();

            string result =  await response.Content.ReadAsStringAsync();

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NetStandard_Asynchronous_Successful(bool configureAwait)
        {
            var response = await _fixture.Client.GetAsync($"{VALUES_CONTROLLER}/net-standard/async?configAwait={configureAwait}");

            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal($"NetStandard-Asynchronous-ConfigureAwait={configureAwait}", result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NetStandard_Synchronous_Successful(bool configureAwait)
        {
            var response = await _fixture.Client.GetAsync($"{VALUES_CONTROLLER}/net-standard/sync?configAwait={configureAwait}");

            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal($"NetStandard-Synchronous-ConfigureAwait={configureAwait}", result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Fullframework_Synchronous_Successful(bool configureAwait)
        {
            var response = await _fixture.Client.GetAsync($"{VALUES_CONTROLLER}/full-framework/sync?configAwait={configureAwait}");

            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal($"FullFramework-Synchronous-ConfigureAwait={configureAwait}", result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Fullframework_Asynchronous_Successful(bool configureAwait)
        {
            var response = await _fixture.Client.GetAsync($"{VALUES_CONTROLLER}/full-framework/async?configAwait={configureAwait}");

            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal($"FullFramework-Asynchronous-ConfigureAwait={configureAwait}", result);
        }
    }
}
