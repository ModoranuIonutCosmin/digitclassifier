using System.Threading.Tasks;
using Application.Models.Login;

namespace Application.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> LogUser(LoginRequest request);
    }
}
