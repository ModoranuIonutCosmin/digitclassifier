using DataAcces.Entities;
using DataAcces.Persistence.Context;
using DataAcces.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Data
{
    public class UserRepositoryTests
    {
        private readonly DITests diKernel;
        private readonly IUserRepository _repository;
        private readonly DatabaseContext databaseContext;

        public UserRepositoryTests()
        {
            diKernel = new DITests();
            _repository = diKernel.ResolveService<IUserRepository>();
            databaseContext = diKernel.ResolveService<DatabaseContext>();
        }

        [Fact]
        public async Task Given_UserRepository_When_GetByUsernameIsCalled_Then_ShouldReturnUserIfExistsInDB()
        {
            //ARRANGE
            //ACT
            var queriedUser = await _repository.GetByUsernameAsync("jhnny101");

            //ASSERT
            Assert.NotNull(queriedUser);
        }

        [Fact]
        public async Task Given_UserRepository_When_User_GetByEmail_Called_Then_ShouldContainUserPersistedInDB()
        {
            //ARRANGE
            var user = new User
            {
                Id = Guid.Parse("c74faa1b-3f57-41f3-806c-1d7710faa88a"),
                UserName = "jhnny101",
                FirstName = "Sefanu",
                LastName = "Johnny",
                Password = "parola",
                Email = "johnny@boss.com"
            };

            //ACT
            await _repository.AddAsync(user);

            //ASSERT
            Assert.True(databaseContext.Users.Any(u => u.UserName == user.UserName && u.Email == user.Email));
        }
    }
}
