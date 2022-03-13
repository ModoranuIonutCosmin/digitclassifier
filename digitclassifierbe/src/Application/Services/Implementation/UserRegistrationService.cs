using Application.Exceptions;
using Application.Models.Registration;
using AutoMapper;
using DataAcces.Entities;
using DataAcces.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSyntaxValidator _emailSyntaxValidator;
        private readonly IMapper _mapper;

        public UserRegistrationService(IUserRepository userRepository,
            IEmailSyntaxValidator emailSyntaxValidator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _emailSyntaxValidator = emailSyntaxValidator;
            _mapper = mapper;
        }

        public async Task<RegistrationResponseModel> RegisterUser(RegistrationRequestModel requestModel)
        {
            if (requestModel is null)
            {
                throw new ArgumentNullException(nameof(requestModel));
            }

            if (requestModel.FirstName is null || requestModel.LastName is null)
            {
                throw new ArgumentException("firstName and lastName body properties must not be null");
            }

            if (!_emailSyntaxValidator.IsEmailValid(requestModel.Email))
                throw new FormatException("Invalid email syntax");

            bool userNameTaken = (await _userRepository.GetByUsernameAsync(requestModel.UserName)) != null;
            bool emailTaken = (await _userRepository.GetByEmail(requestModel.Email)) != null;

            if (userNameTaken)
                throw new UserAlreadyExistsException($"UserName {requestModel.UserName} is taken.");

            if (emailTaken)
                throw new UserEmailTakenException($"Email {requestModel.Email} is bound to another account!");

            await _userRepository.AddAsync(_mapper.Map<User>(requestModel));

            return _mapper.Map<RegistrationResponseModel>(requestModel);
        }
    }
}
