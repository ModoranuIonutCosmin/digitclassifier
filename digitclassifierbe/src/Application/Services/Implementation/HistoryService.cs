using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.History;
using DataAcces.Entities;
using DataAcces.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Features;

namespace Application.Services.Implementation
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public async Task<HistoryResponseFull> GetHistoryForUserAsync(User currentUser, HistoryRequest request)
        {
            List<History> historyList =  new List<History>(await _historyRepository.GetHistoryForUserId(currentUser.Id,request.Filter,request.SortOrder));
            List<HistoryResponse> response = new List<HistoryResponse>();
            int firstIndex = request.PageNumber * request.ElementsPerPage;
            firstIndex = firstIndex < historyList.Count ? firstIndex : historyList.Count;
            int lastIndex = firstIndex + request.ElementsPerPage;
            lastIndex = lastIndex < historyList.Count ? lastIndex : historyList.Count;
            for (int index = firstIndex; index < lastIndex; index++)
            {
                HistoryResponse historyResponse = new HistoryResponse()
                {
                    Id = historyList[index].Id,
                    Image = historyList[index].Image,
                    DateTime = historyList[index].DateTime,
                    PredictedDigit = historyList[index].PredictedDigit,
                    PredictionProbability = historyList[index].PredictionProbability,
                    IsFavorite = historyList[index].IsFavorite
                };
                response.Add(historyResponse);
            }
            var pages = historyList.Count / request.ElementsPerPage;
            var reminder = historyList.Count % request.ElementsPerPage;
            if (reminder != 0)
            {
                pages = pages + 1;
            }
            var responseFull = new HistoryResponseFull()
            {
                HistoryResponseList = response,
                NumberOfPages = pages
            };

            return responseFull;
        }

        async Task<HistoryResponse> IHistoryService.DeleteHistoryEntry(Guid id)
        {
            History entity = await _historyRepository.GetByIdAsync(id);
            await _historyRepository.DeleteAsync(entity);
            var response = new HistoryResponse
            {
                Id = entity.Id,
                Image = entity.Image,
                DateTime = entity.DateTime,
                PredictedDigit = entity.PredictedDigit,
                PredictionProbability = entity.PredictionProbability,
                IsFavorite = entity.IsFavorite
            };
            return response;
        }
    }
}
