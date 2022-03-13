using DataAcces.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = (User)context.HttpContext.Items["User"];
            if (account == null)
            {
                // not logged in
                throw new UnauthorizedAccessException("Unauthorized");
            }
        }
    }
}