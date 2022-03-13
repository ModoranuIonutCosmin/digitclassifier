using Newtonsoft.Json;
using System;
using System.IO;

namespace Api.ResponseHelpers
{
    public static class ApiResponseExtensionMethods
    {
        public static string OriginalResponseToApiResponse(this MemoryStream redirectStream, Exception exception = null)
        {
            redirectStream.Position = 0;
            string originalResponse = new StreamReader(redirectStream).ReadToEnd();

            object originalData = JsonConvert.DeserializeObject(originalResponse);

            return JsonConvert.SerializeObject(new ApiResponse()
            {
                ErrorMessage = exception?.Message,
                Response = originalData
            });
        }
    }
}
