using Api.Controllers;
using Application.Models.Registration;
using Application.Services;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.API
{
    public class RegisterControllerTests
    {
        public readonly RegisterController controller;
        public readonly IUserRegistrationService registrationService;
        public RegisterControllerTests()
        {
            registrationService = A.Fake<IUserRegistrationService>();

            controller = new RegisterController(registrationService);
        }

        [Fact]
        public async Task Given_RegisterController_When_RegisterIsCalled_Then_ShouldCallRegisterUserOnce()
        {
            var registerRequest = new RegistrationRequestModel()
            {
                UserName = "NIKU43",
                Email = "aaa@gmail.com",
                FirstName = "Nicu",
                LastName = "B.",
                Password = "passwd"
            };

            await controller.Register(registerRequest);

            A.CallTo(() => registrationService.RegisterUser(registerRequest))
                .MustHaveHappenedOnceExactly();
        }


        
    }
}
