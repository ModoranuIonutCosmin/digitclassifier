using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using FizzWare.NBuilder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PredictionHelpers.Services;

namespace Api.IntegrationTests
{
    [SetUpFixture]
    public class BaseOneTimeSetup
    {
        protected IHost Host;
        protected HttpClient Client;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTestsAsync()
        {
            Host = await GetNewHostAsync();

            Client = await GetNewClient(Host);
        }

        protected static async Task<IHost> GetNewHostAsync()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                    webHost.ConfigureAppConfiguration((_, configBuilder) =>
                    {
                        configBuilder.AddInMemoryCollection(
                            new Dictionary<string, string>
                            {
                                ["Database:UseInMemoryDatabase"] = "true",

                            });
                    }); ;

                    webHost.ConfigureTestServices(services =>
                    {
                        services.AddScoped<IPredictionService, PredictionService>();
                    });
                });

            var host = await hostBuilder.StartAsync();


            return host;
        }

        private static async Task<HttpClient> GetNewClient(IHost host)
        {
            var configuration = host.Services.GetRequiredService<IConfiguration>();


            var client = host.GetTestClient();

            return client;
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        { }
    }
}