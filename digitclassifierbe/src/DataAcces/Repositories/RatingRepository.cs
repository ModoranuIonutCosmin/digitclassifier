using DataAcces.Entities;
using DataAcces.Persistence.Context;
using DataAcces.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DataAcces.Repositories
{
    public class RatingRepository : Repository<PredictionRating>, IRatingRepository
    {
        public RatingRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public async Task<PredictionRating> GetPredictionByUserAndPredictionId(Guid userId, Guid predictionId)
        {
            return await DatabaseContext.Ratings
                .Include(rating => rating.Prediction)
                .Where(r => r.UserId.Equals(userId) && r.Prediction.Id.Equals(predictionId))
                .SingleOrDefaultAsync();
        }

 

        public async Task<decimal> GetAverageRating(Guid predictionId)
        {
            List<int> ratings = await DatabaseContext.Ratings
                .Where(r => r.Prediction.Id == predictionId)
                .Select(r => r.Stars)
                .ToListAsync();

            if (ratings.Count == 0)
            {
                return 0;
            }

            return (decimal) ratings.Average();
        }

        public async Task<int> GetRatingsCount(Guid predictionId)
        {
            return await DatabaseContext.Ratings
                .Where(r => r.Prediction.Id == predictionId)
                .CountAsync();
        }

    }
}
