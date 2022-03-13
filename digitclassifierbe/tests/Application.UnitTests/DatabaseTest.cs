using DataAcces.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.UnitTests
{
    public class DatabaseTest : IDisposable
    {
        protected readonly DatabaseContextTest context;

        public DatabaseTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase("TestDatabase").Options;
            context = new DatabaseContextTest(options);
            context.Database.EnsureCreated();
            DatabaseInitializer.Initialize(context);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
