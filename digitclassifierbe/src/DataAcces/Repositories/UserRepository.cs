using DataAcces.Entities;
using DataAcces.Persistence.Context;
using DataAcces.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcces.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException($"username can't be null");

            return await DatabaseContext.Users.Where(user => user.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException($"email can't be null");
            }

            return await DatabaseContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}
