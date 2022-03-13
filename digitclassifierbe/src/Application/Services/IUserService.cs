using System.Threading.Tasks;
using DataAcces.Entities;

namespace Application.Services
{
    public interface IUserService
    {
        Task<User>  GetUserByUsername(string username);
    }
}