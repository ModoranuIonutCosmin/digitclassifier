using System;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entities.Common.BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
    }
}
