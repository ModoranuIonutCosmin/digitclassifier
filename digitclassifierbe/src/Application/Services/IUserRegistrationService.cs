using Application.Models.Registration;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserRegistrationService
    {
        Task<RegistrationResponseModel> RegisterUser(RegistrationRequestModel requestModel);
    }
}