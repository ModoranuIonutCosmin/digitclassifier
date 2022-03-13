using System.Threading.Tasks;
using Application.Services;
using Application.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Api.Middleware
{

    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;


        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            if (token != null && token != "")
            {
                var tokenHandler = new JwtService();
                var username = tokenHandler.ValidateToken(token);
                if (username == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return; 
                }
                var user = await userService.GetUserByUsername(username);
                if (user == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                context.Items["User"] = user;
            }
            await _next(context);
        }
    }
}