using Application.Models.Registration;
using Application.Services;
using DataAcces.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Service
{
    public class RegistrationServiceTests
    {
        private readonly DITests diKernel;
        private readonly IUserRegistrationService registrationService;
        private readonly DatabaseContext dbContext;

        public RegistrationServiceTests()
        {
            diKernel = new DITests();

            registrationService = diKernel.ResolveService<IUserRegistrationService>();
            dbContext = diKernel.ResolveService<DatabaseContext>();
        }

        [Fact]
        public async Task Given_RegisterService_When_RegisterIsCalledWithInvalidEmail_Then_ShouldBeUnsuccessful()
        {
            var registerRequest = new RegistrationRequestModel()
            {
                UserName = "Nelw43",
                Email = "zzz",
                FirstName = "Nicu",
                LastName = "B.",
                Password = "passwd"
            };

            await Assert.ThrowsAsync<FormatException>(async 
                () => { await registrationService.RegisterUser(registerRequest); });
        }

        [Fact]
        public async Task Given_RegisterService_When_RegisterIsCalledWithValidRequest_Then_UserShouldBePersistedInDB()
        {
            
            var registerRequest = new RegistrationRequestModel()
            {
                UserName = "NIKU43",
                Email = "aaa@gmail.com",
                FirstName = "Nicu",
                LastName = "B.",
                Password = "passw0rd"
            };

            //ACT
            await registrationService.RegisterUser(registerRequest);

            //ASSERT
            bool userPersisted = await dbContext.Users
                .AnyAsync(k => k.Email == registerRequest.Email && k.UserName == registerRequest.UserName);

            Assert.True(userPersisted);
        }
    }
}
