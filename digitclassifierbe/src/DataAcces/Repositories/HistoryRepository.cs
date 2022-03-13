using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcces.Entities;
using DataAcces.Persistence.Context;
using DataAcces.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAcces.Repositories
{
    public class HistoryRepository : Repository<History>, IHistoryRepository
    {
        public HistoryRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IEnumerable<History>> GetHistoryForUserId(Guid id,String filter,String sortOrder)
        {
            if(Guid.Empty == id)
                throw new ArgumentNullException($"{nameof(GetHistoryForUserId)} parameter can't be null");
            var result = new List<History>();
            switch (filter)
            {
                case "dateTime" : 
                    result=await DatabaseContext.Histories.Where(history => history.UserId == id)
                        .OrderBy(history => history.DateTime).ToListAsync();
                    break;
                case "predictedDigit" : 
                    result=await DatabaseContext.Histories.Where(history => history.UserId == id)
                        .OrderBy(history => history.PredictedDigit).ToListAsync();
                    break;
                case "predictionProbability" : 
                    result=await DatabaseContext.Histories.Where(history => history.UserId == id)
                        .OrderBy(history => history.PredictionProbability).ToListAsync();
                    break;
            }

            if (sortOrder.Equals("desc"))
            {
                result.Reverse();
            }
            return result;
        }

        public async Task<IEnumerable<History>> GetFavoritesAsync(Guid id,String filter,String sortOrder)
        {
            if(Guid.Empty == id)
                throw new ArgumentNullException($"{nameof(GetHistoryForUserId)} parameter can't be null");
            var result = new List<History>();
            switch (filter)
            {
                case "dateTime" : 
                    result=await DatabaseContext.Histories.Where(history => history.UserId == id).Where(isfav=>isfav.IsFavorite)
                    .OrderBy(history => history.DateTime).ToListAsync();
                    break;
                case "predictedDigit" : 
                    result=await DatabaseContext.Histories.Where(history => history.UserId == id).Where(isfav=>isfav.IsFavorite)
                        .OrderBy(history => history.PredictedDigit).ToListAsync();
                    break;
                case "predictionProbability" : 
                    result=await DatabaseContext.Histories.Where(history => history.UserId == id).Where(isfav=>isfav.IsFavorite)
                        .OrderBy(history => history.PredictionProbability).ToListAsync();
                    break;
            }

            if (sortOrder.Equals("desc"))
            {
                result.Reverse();
            }
            return result;        }

        public async Task<History> AddFavoritesAsync(Guid id)
        {
            History history = await DatabaseContext.Histories.FindAsync(id);
            if (history == null)
            {
                throw new ArgumentNullException($"{nameof(AddFavoritesAsync)} not found");

            }
            history.IsFavorite = true;
            await DatabaseContext.SaveChangesAsync();
            return history;

        }

        public async Task<List<History>> GetHistoriesPredictionsPaginated(int page, int count)
        {
            page--;

            List<History> predictions = await DatabaseContext.Histories
                .Include(history => history.Ratings)
                .OrderByDescending(entry => entry.DateTime)
                .Skip(page * count)
                .Take(count)
                .ToListAsync();

            return predictions;
        }

        public async Task<History> DeleteFavoritesAsync(Guid id)
        {
            History history = await DatabaseContext.Histories.FindAsync(id);

            if (history == null)
            {
                throw new ArgumentNullException($"{nameof(AddFavoritesAsync)} not found");
            }

            history.IsFavorite = false;
            await DatabaseContext.SaveChangesAsync();
            return history;
        }

        public async Task<int> GetPredictionsCount()
        {
            return await DatabaseContext.Histories.CountAsync();
        }

    }
}
