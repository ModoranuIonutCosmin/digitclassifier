using Api.ResponseHelpers;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            Stream originalStream = httpContext.Response.Body;

            using var redirectStream = new MemoryStream();
            httpContext.Response.Body = redirectStream;

            try
            {
                await _next(httpContext);

                var newResponse = redirectStream.OriginalResponseToApiResponse();

                using var outputStream = new MemoryStream(Encoding.UTF8.GetBytes(newResponse));

                await outputStream.CopyToAsync(originalStream);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request threw exception {ex.GetType()}");

                httpContext.Response.Body = originalStream;

                await HandleExceptionAsync(httpContext, ex);
            }

            finally
            {
                httpContext.Response.Body = originalStream;
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Stream originalStream = context.Response.Body;

            context.Response.ContentType = "application/json";

            string newReponse = JsonConvert.SerializeObject(new ApiResponse() 
            { 
                ErrorMessage = exception?.Message 
            });

            if(exception is ArgumentException || exception is ArgumentNullException ||
                exception is NullReferenceException)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            if (exception is UserAlreadyExistsException || exception is UserEmailTakenException)
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;

            if(exception is AuthenticationFailedException)
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            if (exception is UserNotFoundException)
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;


            using var outputStream = new MemoryStream(Encoding.UTF8.GetBytes(newReponse));

            await outputStream.CopyToAsync(originalStream);
        }
    }
}
