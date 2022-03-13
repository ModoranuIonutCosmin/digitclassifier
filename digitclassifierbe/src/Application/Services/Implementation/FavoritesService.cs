using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.History;
using DataAcces.Entities;
using DataAcces.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class FavoritesService: IFavoritesService
    {
        private readonly IHistoryRepository _historyRepository;

        public FavoritesService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public async Task<FavoritesResponseFull> GetFavoritesAsync(User currentUser, FavoritesRequest request)
        {
            List<History> historyList =  new List<History>(await _historyRepository.GetFavoritesAsync(currentUser.Id,request.Filter,request.SortOrder));
            List<FavoritesResponse> response = new List<FavoritesResponse>();
            int firstIndex = request.PageNumber * request.ElementsPerPage;
            firstIndex = firstIndex < historyList.Count ? firstIndex : historyList.Count;
            int lastIndex = firstIndex + request.ElementsPerPage;
            lastIndex = lastIndex < historyList.Count ? lastIndex : historyList.Count;
            for (int index = firstIndex; index < lastIndex; index++)
            {
                FavoritesResponse favoritesResponse = new FavoritesResponse()
                {
                    Id = historyList[index].Id,
                    Image = historyList[index].Image,
                    DateTime = historyList[index].DateTime,
                    PredictedDigit = historyList[index].PredictedDigit,
                    PredictionProbability = historyList[index].PredictionProbability
                };
                response.Add(favoritesResponse);
            }
            var pages = historyList.Count / request.ElementsPerPage;
            var reminder = historyList.Count % request.ElementsPerPage;
            if (reminder != 0)
            {
                pages = pages + 1;
            }
            var responseFull = new FavoritesResponseFull()
            {
                FavoritesResponseList = response,
                NumberOfPages = pages
            };

            return responseFull;
        }

        public async Task<DeleteFavoriteResponse> DeleteFavoriteEntry(Guid id)
        {
            History history = await _historyRepository.DeleteFavoritesAsync(id);
            var response = new DeleteFavoriteResponse()
            {
                Id = history.Id,
                Image = history.Image,
                DateTime = history.DateTime,
                PredictedDigit = history.PredictedDigit,
                PredictionProbability = history.PredictionProbability
            };
            return response;
        }

        public async Task<AddFavoriteResponse> AddFavoriteEntry(Guid id)
        {
            History history = await _historyRepository.AddFavoritesAsync(id);
            var response = new AddFavoriteResponse()
            {
                Id = history.Id,
                Image = history.Image,
                DateTime = history.DateTime,
                PredictedDigit = history.PredictedDigit,
                PredictionProbability = history.PredictionProbability
            };
            return response;
        }
    }
}