using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAcces.Entities;

namespace DataAcces.Repositories.Interfaces
{
    public interface IHistoryRepository : IRepository<History>
    {
        Task<IEnumerable<History>> GetHistoryForUserId(Guid id,string filter,string sortOrder);
        Task<IEnumerable<History>> GetFavoritesAsync(Guid id,string filter,string sortOrder);
        Task<History> AddFavoritesAsync(Guid id);
        Task<History> DeleteFavoritesAsync(Guid id);
        Task<List<History>> GetHistoriesPredictionsPaginated(int page, int count);
        Task<int> GetPredictionsCount();
    }
}
