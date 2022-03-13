using DataAcces.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests
{
    public class DatabaseContextTest : DatabaseContext
    {
        public DatabaseContextTest(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
