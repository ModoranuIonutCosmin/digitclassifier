using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Api.ResponseHelpers
{
    public static class ProblemDetailsExtensions
    {

        public const string EXCEPTION_SUFFIX = "Exception";
        public static ProblemDetails MapToProblemDetailsWithStatusCode(this Exception details, HttpStatusCode statusCode)
        {

            string exceptionTitle = details.GetType().Name;

            if (exceptionTitle.EndsWith(EXCEPTION_SUFFIX))
            {
                exceptionTitle = exceptionTitle[..exceptionTitle.LastIndexOf(EXCEPTION_SUFFIX)];
            }

            return new ProblemDetails
            {
                Detail = details.Message,
                Status = (int)statusCode,
                Title = exceptionTitle,
                Type = $"https://httpstatuses.com/{(int) statusCode}",
            };
        }

    }
}
