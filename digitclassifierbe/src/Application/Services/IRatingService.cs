using Application.Models.Ratings;
using DataAcces.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IRatingService
    {
        Task<RatingsStatsFullResponse> GetAllRatingStats(int page, int count);
        Task<RatingStats> GetRatingStats(Guid predictionId);
        Task<OldRating> GetUserRatingForPrediction(User requestingUser, Guid predictionId);
        Task<RateRequest> RatePrediction(User userRequesting, RateRequest rateRequest);
    }
}