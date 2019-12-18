using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SellerCloud.AspNetCore.Api;
using System;
using System.Net.Http;
using Xunit;

namespace SellerCloud.Presentation.Multithreading.Tests
{
    /// <summary>
    /// The fixture shared between all integration tests so the test server and client are
    /// instantiated only once, before any test starts and disposed of only after the last
    /// test in the collection (all classes decorated with
    /// [Collection(Trait.INTEGRATION_TESTS)] attribute) is executed.
    /// </summary>
    public class IntegrationTestsFixture : IDisposable
    {
        private static readonly string BASE_URI = "http://localhost:5000";

        #region Public Constructors

        public IntegrationTestsFixture()
        {
            WebHostInstance = WebHost
                .CreateDefaultBuilder(null)
                .UseEnvironment("Test")
                .UseStartup<Startup>()
                .UseKestrel()
                .UseUrls(BASE_URI)
                .Build();

            WebHostInstance.Start();

            Client = new HttpClient
            {
                BaseAddress = new Uri(BASE_URI),
            };
        }

        #endregion Public Constructors

        /// <summary>
        /// Disposes the client and the test server after all tests are executed
        /// </summary>
        public void Dispose()
        {
            Client?.Dispose();
        }


        #region Public Properties

        /// <summary>
        /// Gets the test HTTP client.
        /// </summary>
        public HttpClient Client { get; private set; }

        public static IWebHost WebHostInstance { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// Decorate a test class with: [Collection(Traits.INTEGRATION_TESTS)] to share the same
    /// test server and http client
    /// </summary>
    [CollectionDefinition(Traits.INTEGRATION_TESTS)]
    public class TestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture>
    {
        // This class has no code, and is never created. Its purpose is simply to be the
        // place to apply [CollectionDefinition] and all the ICollectionFixture<> interfaces.
    }
}