using System.Threading.Tasks;
using DataAcces.Entities;

namespace DataAcces.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUsernameAsync(string username);
    }
}
