using System.Threading.Tasks;
using DataAcces.Entities;
using DataAcces.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user=await _userRepository.GetByUsernameAsync(username);
             return user;

        }
    }
}