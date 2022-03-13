using Application.Exceptions;
using Application.Models.Login;
using DataAcces.Repositories;
using System;
using System.Threading.Tasks;
using DataAcces.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;  
            _jwtService = new JwtService();
        }

        public async Task<LoginResponse> LogUser(LoginRequest request)
        {
            if (request == null)
                throw new ArgumentNullException($"request can't be null");
            _ = await IsUserOk(request.UserName, request.Password);

            LoginResponse response = new LoginResponse { 
                JWTToken = _jwtService.GenerateJwt(request)
            };

            return response;
        }

        private async Task<bool> IsUserOk(string userName, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(userName);
            if (user == null)
                throw new UserNotFoundException($"{userName} doesn't exist in db");
            if (user.Password != password)
                throw new AuthenticationFailedException($"{userName} wrong authentication data");
            return true;
        }
    }
}
