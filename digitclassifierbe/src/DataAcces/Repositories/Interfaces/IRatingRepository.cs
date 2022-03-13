using DataAcces.Entities;
using System;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Interfaces
{
    public interface IRatingRepository : IRepository<PredictionRating>
    {
        Task<decimal> GetAverageRating(Guid predictionId);
        Task<PredictionRating> GetPredictionByUserAndPredictionId(Guid userId, Guid predictionId);
        Task<int> GetRatingsCount(Guid predictionId);
    }
}
