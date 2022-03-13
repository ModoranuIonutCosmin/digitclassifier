using DataAcces.Entities.Common;
using DataAcces.Persistence.Context;
using DataAcces.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace DataAcces.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext DatabaseContext;

        protected Repository(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(T)} entity can't be null");

            await DatabaseContext.AddAsync(entity);

            await DatabaseContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(T)} entity can't be null");
            DatabaseContext.Remove(entity);
            await DatabaseContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException($"{nameof(GetByIdAsync)} id can't be null");
            return await DatabaseContext.FindAsync<T>(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(T)} entity can't be null");
            DatabaseContext.Update(entity);
            await DatabaseContext.SaveChangesAsync();
            return entity;
        }
    }
}
