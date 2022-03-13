using Application.Models.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Services;

namespace Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var LoginResponse = await _loginService.LogUser(request);
            IActionResult response = LoginResponse == null ? Unauthorized() : Ok(LoginResponse);
            return response;
        }
    }
}
