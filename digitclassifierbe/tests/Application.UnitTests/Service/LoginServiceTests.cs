using Application.Models.Login;
using Application.Services;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Service
{
    public class LoginServiceTests
    {
        private readonly DITests diKernel;

        public LoginServiceTests()
        {
            diKernel = new DITests();
        }

        [Fact]
        public async Task Given_LoginController_When_LoginIsCalled_Then_Should_CallLogUser_InRepository()
        {
            //ARRANGE
            var loginService = diKernel.ResolveService<ILoginService>();

            var loginRequest = new LoginRequest
            {
                UserName = "jhnny101",
                Password = "parola",
            };

            //ACT
            var loginResponse = await loginService.LogUser(loginRequest);

            //ASSERT
            Assert.False(string.IsNullOrEmpty(loginResponse.JWTToken));
        }

    }
}
