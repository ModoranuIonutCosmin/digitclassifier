using DataAcces.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.UnitTests.Helpers
{
    public static class MockHttpContextHelpers
    {
        public static User MockUserAuthorizationForController(this ControllerBase controller)
        {
            var user = new User();
            controller.ControllerContext = new ControllerContext();

            if (controller.HttpContext == null)
            {
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
            }

            controller.HttpContext.Items["User"] = user;

            return user;
        }
    }
}
