using System;
using Application.Models.Image;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAcces.Entities;
using DataAcces.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class ImageService : IImageService
    {
        private readonly HttpClient httpClient;
        static string URL = "http://localhost:6969/api/ml";
        private readonly IHistoryRepository _historyRepository;

        public ImageService(IHistoryRepository historyRepository, HttpClient httpClient)
        {
            _historyRepository = historyRepository;
            this.httpClient = httpClient;
        }

        public async Task<ImageResponse> Predict(User user, ImageRequest request)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env != "Development")
            {
                URL = "http://digitclassifierml.herokuapp.com/api/ml";
            }
            var httpResponse = await httpClient.PostAsync(URL, content);
            ImageResponse response = JsonConvert
                .DeserializeObject<ImageResponse>(httpResponse.Content.ReadAsStringAsync().Result);

            var historyEntry = new History()
            {
                Image = request.Base64Image,
                DateTime = DateTime.Now,
                UserId = user.Id,
                PredictedDigit = response.DigitPredicted,
                PredictionProbability = response.PredictionLikelihood,
                IsFavorite = false
            };
            History newHistoryEntry = await _historyRepository.AddAsync(historyEntry);

            response.HistoryEntryId = newHistoryEntry.Id;

            return response;
        }
    }
}
