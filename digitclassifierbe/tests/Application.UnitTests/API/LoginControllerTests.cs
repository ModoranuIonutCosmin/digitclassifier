using Api.Controllers;
using Application.Models.Login;
using Application.Services;
using FakeItEasy;
using Xunit;

namespace Application.UnitTests.API
{
    public class LoginControllerTests
    {
        private readonly LoginController _controller;
        private readonly ILoginService _service;

        public LoginControllerTests()
        {
            _service = A.Fake<ILoginService>();
            _controller = new LoginController(_service);
        }

        [Fact]
        public async void Given_LoginController_When_LoginIsCalled_Then_Should_CallLogUser_InRepository()
        {
            var request = new LoginRequest
            {
                UserName = "johnny",
                Password = "parola"
            };
            var response = await _controller.Login(request);
            A.CallTo(() =>_service.LogUser(request)).MustHaveHappenedOnceExactly();
        }
    }
}
