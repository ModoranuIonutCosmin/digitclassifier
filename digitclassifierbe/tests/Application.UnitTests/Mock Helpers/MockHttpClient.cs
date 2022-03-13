using Application.UnitTests.Mock_Helpers.HttpClientMessageHandler;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Application.UnitTests.Mock_Helpers
{
    public static class MockHttpClient
    {
        public static IServiceCollection AddMockHttpClient(this IServiceCollection services, object value)
        {
            services.AddSingleton(provider => new HttpClient(new MockHttpMessageHandler(value)));

            return services;
        }
    }
}
