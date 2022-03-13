using Api.ResponseHelpers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Extension_Methods
{
    public class ApiResponseHelpersTests
    {
        [Fact]
        public void Given_ResponseMemoryStream_When_OriginalResponseToApiResponseExtensionMethodIsCalledWithoutException_Then_ReturnedResponseIsWrappedInsideApiResponse()
        {
            //ARRANGE
            MemoryStream mockResponseStream = new MemoryStream();
            Exception exception = null;

            //ACT
            var newResponseString = mockResponseStream.OriginalResponseToApiResponse(exception);

            //ASSERT
            ApiResponse newResponse = JsonConvert.DeserializeObject<ApiResponse>(newResponseString);

            Assert.True(newResponse.Successful);
        }


        [Fact]
        public void Given_ResponseMemoryStream_When_OriginalResponseToApiResponseExtensionMethodIsCalledWithException_Then_ReturnedResponseIsWrappedInsideApiResponse()
        {
            //ARRANGE
            MemoryStream mockResponseStream = new();
            Exception exception = new Exception("Failed operation");

            //ACT
            var newResponseString = mockResponseStream.OriginalResponseToApiResponse(exception);

            //ASSERT
            ApiResponse newResponse = JsonConvert.DeserializeObject<ApiResponse>(newResponseString);

            Assert.False(newResponse.Successful);
        }
    }
}
