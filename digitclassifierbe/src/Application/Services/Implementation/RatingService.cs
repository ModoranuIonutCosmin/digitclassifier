using Application.Exceptions;
using Application.Models.History;
using Application.Models.Ratings;
using AutoMapper;
using DataAcces.Entities;
using DataAcces.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class RatingService : IRatingService
    {
        private readonly IHistoryRepository historyRepository;
        private readonly IRatingRepository ratingRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public RatingService(IHistoryRepository historyRepository,
            IRatingRepository ratingRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.historyRepository = historyRepository;
            this.ratingRepository = ratingRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<RateRequest> RatePrediction(User userRequesting, RateRequest rateRequest)
        {
            History predictionHistoryEntry = await historyRepository
                .GetByIdAsync(Guid.Parse(rateRequest.HistoryId));

            if (predictionHistoryEntry == null)
            {
                throw new PredictionNotFoundException("No such prediction available for rating!");
            }

            PredictionRating existingRating = await ratingRepository
                .GetPredictionByUserAndPredictionId(userRequesting.Id, predictionHistoryEntry.Id);

            // Check if a prediction is already submited for this user and this prediction pair.
            if (existingRating != null)
            {
                throw new RatingAlreadyExistsException("Rating was already submited.");
            }

            if (rateRequest.StarsAmount < 0 || rateRequest.StarsAmount > 5)
            {
                throw new InvalidRatingValueException("Improper rating value. Valid options 0 - 5.");
            }

            PredictionRating newRatingEntry = new PredictionRating
            {
                Prediction = predictionHistoryEntry,
                Stars = rateRequest.StarsAmount,
                User = await userRepository.GetByIdAsync(userRequesting.Id),
            };

            await ratingRepository.AddAsync(newRatingEntry);

            return rateRequest;
        }

        public async Task<RatingStats> GetRatingStats(Guid predictionId)
        {
            History predictionEntity = await historyRepository.GetByIdAsync(predictionId);

            return new RatingStats
            {
                AverageRating = await ratingRepository.GetAverageRating(predictionId),
                Prediction = mapper.Map<History, HistoryResponse>(predictionEntity),
                RatingVotesCount = await ratingRepository.GetRatingsCount(predictionId),
            };
        }

        public async Task<OldRating> GetUserRatingForPrediction(User requestingUser, Guid predictionId)
        {
            History predictionEntity = await historyRepository.GetByIdAsync(predictionId);

            if (predictionEntity == null)
            {
                throw new RatingDoesNotExistsException("Prediction doesn' exist!");
            }

            var storedRating = await ratingRepository.GetPredictionByUserAndPredictionId(requestingUser.Id, predictionId);

            return new()
            {
                RatingStars = storedRating?.Stars ?? 0,
            };
        }

        public async Task<RatingsStatsFullResponse> GetAllRatingStats(int page, int count)
        {
            List<History> predictions = await historyRepository.GetHistoriesPredictionsPaginated(page, count);

            return new RatingsStatsFullResponse()
            {
                PredictionsStats = predictions.Select(prediction =>
                {
                    decimal rating = 0;

                    if (prediction.Ratings.Any())
                    {
                        rating = (decimal)prediction.Ratings.Average(e => e.Stars);
                    }

                    return new RatingStats
                    {
                        AverageRating = rating,
                        Prediction = mapper.Map<History, HistoryResponse>(prediction),
                        RatingVotesCount = prediction.Ratings.Count
                    };

                }).ToList(),
                TotalPredictionsCount = await historyRepository.GetPredictionsCount()
            };
        }
    }
}
