using Application.Models.Registration;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Api.ResponseHelpers;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRegistrationService _registrationService;

        public RegisterController(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestModel request)
        {
            return Created($"api/users/{request.UserName}", await _registrationService.RegisterUser(request));
        }
    }
}
