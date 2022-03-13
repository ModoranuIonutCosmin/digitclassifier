using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Mock_Helpers.HttpClientMessageHandler
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private string mockResponse;
        public MockHttpMessageHandler(object value)
        {
            mockResponse  = JsonConvert.SerializeObject(value);
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
         => Task.FromResult(new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent(mockResponse) });
    }
}
