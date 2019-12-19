using SellerCloud.AspNetCore.Api;
using SellerCloud.Presentation.Multithreading.Tests.Extensions;
using System;
using System.Net;
using System.Threading;
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
            Tuple<HttpStatusCode, RestResult<SynchronizationContext>> result = await _fixture.Client
                .GetAsync(VALUES_CONTROLLER)
                .ContinueWithResultAsync<RestResult<SynchronizationContext>>();

            Assert.Equal(HttpStatusCode.OK, result.Item1);
            Assert.Null(result.Item2.Value);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NetStandard_Asynchronous_Successful(bool configureAwait)
        {
            Tuple<HttpStatusCode, RestResult<object>> result = await _fixture.Client
                .GetAsync($"{VALUES_CONTROLLER}/net-standard/async?configAwait={configureAwait}")
                .ContinueWithResultAsync<RestResult<object>>();

            Assert.Equal(HttpStatusCode.OK, result.Item1);
            Assert.Equal($"NetStandard-Asynchronous-ConfigureAwait={configureAwait}", result.Item2.Message);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NetStandard_Synchronous_Successful(bool configureAwait)
        {
            Tuple<HttpStatusCode, RestResult<object>> result = await _fixture.Client
                .GetAsync($"{VALUES_CONTROLLER}/net-standard/sync?configAwait={configureAwait}")
                .ContinueWithResultAsync<RestResult<object>>();

            Assert.Equal(HttpStatusCode.OK, result.Item1);
            Assert.Equal($"NetStandard-Synchronous-ConfigureAwait={configureAwait}", result.Item2.Message);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Fullframework_Synchronous_Successful(bool configureAwait)
        {
            Tuple<HttpStatusCode, RestResult<object>> result = await _fixture.Client
                .GetAsync($"{VALUES_CONTROLLER}/full-framework/sync?configAwait={configureAwait}")
                .ContinueWithResultAsync<RestResult<object>>();

            Assert.Equal(HttpStatusCode.OK, result.Item1);
            Assert.Equal($"FullFramework-Synchronous-ConfigureAwait={configureAwait}", result.Item2.Message);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Fullframework_Asynchronous_Successful(bool configureAwait)
        {
            HttpStatusCode response = await _fixture.Client
                .GetAsync($"{VALUES_CONTROLLER}/full-framework/async?configAwait={configureAwait}")
                .ContinueWithResultAsync();

            Assert.Equal(HttpStatusCode.OK, response);
        }
    }
}
